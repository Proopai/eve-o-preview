using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace EveOPreview.Configuration.Implementation
{
    // NOTE: implements IDisposable so LightInject disposes the watcher with the singleton.
    sealed class ConfigurationStorage : IConfigurationStorage, IDisposable
    {
        private const string CONFIG_FOLDER_NAME = "EVE-O Preview";
        private const string PROFILES_FOLDER_NAME = "Profiles";
        private const string PROFILE_FILE_EXTENSION = ".json";
        private const string DEFAULT_PROFILE_NAME = "Default";
        private const string ACTIVE_PROFILE_POINTER_NAME = "active-profile.txt";
        private const string LEGACY_CONFIG_FILE_NAME = "EVE-O-Preview.json";
        private const int DEBOUNCE_MS = 400;

        private readonly IAppConfig _appConfig;
        private readonly IThumbnailConfiguration _thumbnailConfiguration;
        private readonly object _ioSyncRoot = new object();

        private FileSystemWatcher _watcher;
        private Timer _debounceTimer;
        private string _lastWrittenHash;
        private string _activeProfileName;

        public ConfigurationStorage(IAppConfig appConfig, IThumbnailConfiguration thumbnailConfiguration)
        {
            this._appConfig = appConfig;
            this._thumbnailConfiguration = thumbnailConfiguration;
            this._activeProfileName = DEFAULT_PROFILE_NAME;
        }

        /// <summary>
        /// Raised on a background thread after the active profile file is edited externally and
        /// successfully reloaded. Subscribers MUST marshal to the UI thread before re-registering
        /// hotkeys / refreshing thumbnails (issue #94).
        /// </summary>
        public event Action ConfigurationReloaded;

        public string ActiveProfileName => this._activeProfileName;

        public void Load()
        {
            string profilesDir = this.GetProfilesDirectory();

            this.MigrateToProfiles(profilesDir);

            string active = this.ReadActiveProfilePointer();
            if (string.IsNullOrEmpty(active) || !File.Exists(this.GetProfilePath(active)))
            {
                active = this.GetProfileNames().FirstOrDefault() ?? DEFAULT_PROFILE_NAME;
            }
            this._activeProfileName = active;

            string activePath = this.GetProfilePath(this._activeProfileName);

            if (File.Exists(activePath))
            {
                string rawData = File.ReadAllText(activePath);
                ApplyRawData(rawData);
                _lastWrittenHash = ComputeHash(rawData);
            }
            else
            {
                // Nothing on disk: write a default profile from the current (default) configuration.
                this._thumbnailConfiguration.ApplyRestrictions();
                this.Save();
            }

            this.WriteActiveProfilePointer(this._activeProfileName);
            this.StartWatching(activePath);
        }

        public void Save()
        {
            string rawData = JsonConvert.SerializeObject(this._thumbnailConfiguration, Formatting.Indented);
            string filename = this.GetProfilePath(this._activeProfileName);

            lock (_ioSyncRoot)
            {
                try
                {
                    // Record the hash *before* writing so the watcher recognises this write
                    // as our own and doesn't treat it as an external edit (issue #94).
                    _lastWrittenHash = ComputeHash(rawData);
                    File.WriteAllText(filename, rawData);
                }
                catch (IOException)
                {
                    // Ignore error if for some reason the updated config cannot be written down.
                }
            }
        }

        private void ApplyRawData(string rawData)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };

            JsonConvert.PopulateObject(rawData, this._thumbnailConfiguration, jsonSerializerSettings);

            // Validate data after loading it.
            this._thumbnailConfiguration.ApplyRestrictions();
        }

        #region Profiles
        public IReadOnlyList<string> GetProfileNames()
        {
            string dir = this.GetProfilesDirectory();
            try
            {
                return Directory.GetFiles(dir, "*" + PROFILE_FILE_EXTENSION)
                    .Select(Path.GetFileNameWithoutExtension)
                    .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            catch (IOException)
            {
                return new List<string>();
            }
        }

        // Creates a new profile as a copy of the current live configuration.
        public bool CreateProfile(string name)
        {
            if (!IsValidProfileName(name))
            {
                return false;
            }

            string path = this.GetProfilePath(name);
            if (File.Exists(path))
            {
                return false;
            }

            string rawData = JsonConvert.SerializeObject(this._thumbnailConfiguration, Formatting.Indented);
            try
            {
                File.WriteAllText(path, rawData);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        public bool DeleteProfile(string name)
        {
            // Never delete the active profile or the last remaining one.
            if (string.Equals(name, this._activeProfileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            string path = this.GetProfilePath(name);
            if (!File.Exists(path) || this.GetProfileNames().Count <= 1)
            {
                return false;
            }

            try
            {
                File.Delete(path);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        // Loads another profile into the live configuration and re-points the watcher.
        public bool SwitchProfile(string name)
        {
            string path = this.GetProfilePath(name);
            if (!File.Exists(path))
            {
                return false;
            }

            string rawData;
            lock (_ioSyncRoot)
            {
                try
                {
                    rawData = File.ReadAllText(path);
                }
                catch (IOException)
                {
                    return false;
                }
            }

            if (!IsValidConfig(rawData))
            {
                return false;
            }

            ApplyRawData(rawData);
            this._activeProfileName = name;
            _lastWrittenHash = ComputeHash(rawData);
            this.WriteActiveProfilePointer(name);
            this.StartWatching(path);

            return true;
        }

        // Resets the active profile's live configuration to factory defaults and saves it.
        public void ResetActiveProfileToDefaults()
        {
            string defaults = JsonConvert.SerializeObject(new ThumbnailConfiguration(), Formatting.Indented);
            ApplyRawData(defaults);
            this.Save();
        }

        // Writes the current live configuration to an arbitrary file the user picked.
        public bool ExportProfile(string destinationPath)
        {
            string rawData = JsonConvert.SerializeObject(this._thumbnailConfiguration, Formatting.Indented);
            try
            {
                File.WriteAllText(destinationPath, rawData);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        // Imports an external config file as a NEW profile (named from the file, de-duplicated).
        // Returns the new profile name, or null on failure. Old-format files are accepted and
        // migrate to the new schema when the profile is activated.
        public string ImportProfile(string sourcePath)
        {
            string rawData;
            try
            {
                rawData = File.ReadAllText(sourcePath);
            }
            catch (IOException)
            {
                return null;
            }

            if (!IsValidConfig(rawData))
            {
                return null;
            }

            string baseName = Path.GetFileNameWithoutExtension(sourcePath);
            if (!IsValidProfileName(baseName))
            {
                baseName = "Imported";
            }

            string name = baseName;
            int suffix = 2;
            while (File.Exists(this.GetProfilePath(name)))
            {
                name = baseName + " (" + suffix + ")";
                suffix++;
            }

            try
            {
                File.WriteAllText(this.GetProfilePath(name), rawData);
                return name;
            }
            catch (IOException)
            {
                return null;
            }
        }
        #endregion

        #region Hot reload (issue #94)
        private void StartWatching(string filename)
        {
            string dir = Path.GetDirectoryName(filename);
            string name = Path.GetFileName(filename);
            if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
            {
                return;
            }

            if (_debounceTimer == null)
            {
                _debounceTimer = new Timer(_ => OnDebouncedChange(), null, Timeout.Infinite, Timeout.Infinite);
            }

            // Re-point the watcher (the active profile file can change when switching profiles).
            _watcher?.Dispose();
            _watcher = new FileSystemWatcher(dir, name)
            {
                // FileName matters: many editors save atomically via temp-file + rename.
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName
            };
            _watcher.Changed += OnRawChange;
            _watcher.Created += OnRawChange;
            _watcher.Renamed += OnRawChange;
            _watcher.EnableRaisingEvents = true;
        }

        // Editors fire several events per save; coalesce them and wait for quiet.
        private void OnRawChange(object sender, FileSystemEventArgs e)
        {
            _debounceTimer?.Change(DEBOUNCE_MS, Timeout.Infinite);
        }

        private void OnDebouncedChange()
        {
            string filename = this.GetProfilePath(this._activeProfileName);
            string rawData;

            lock (_ioSyncRoot)
            {
                try
                {
                    if (!File.Exists(filename))
                    {
                        return;
                    }

                    rawData = File.ReadAllText(filename);
                }
                catch (IOException)
                {
                    // Still locked by the editor — retry shortly.
                    _debounceTimer?.Change(DEBOUNCE_MS, Timeout.Infinite);
                    return;
                }

                // This content is the result of our own Save() — not an external edit.
                if (ComputeHash(rawData) == _lastWrittenHash)
                {
                    return;
                }
            }

            // Validate BEFORE mutating the live config (same philosophy as issue #15).
            if (!IsValidConfig(rawData))
            {
                return;
            }

            try
            {
                ApplyRawData(rawData);
                _lastWrittenHash = ComputeHash(rawData);
            }
            catch (Exception)
            {
                return; // keep the previous good config
            }

            this.ConfigurationReloaded?.Invoke();
        }

        private static bool IsValidConfig(string rawData)
        {
            try
            {
                JsonConvert.DeserializeObject<ThumbnailConfiguration>(rawData);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
        #endregion

        #region Paths & migration (issues #94 / #128 / profiles)
        private string GetConfigDirectory()
        {
            // SpecialFolder.ApplicationData is cross-platform under .NET 8:
            //   Windows -> %APPDATA%, Linux -> ~/.config, Windows-on-Wine -> prefix AppData.
            string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dir = Path.Combine(baseDir, CONFIG_FOLDER_NAME);
            Directory.CreateDirectory(dir);
            return dir;
        }

        private string GetProfilesDirectory()
        {
            string dir = Path.Combine(this.GetConfigDirectory(), PROFILES_FOLDER_NAME);
            Directory.CreateDirectory(dir);
            return dir;
        }

        private string GetProfilePath(string name)
        {
            return Path.Combine(this.GetProfilesDirectory(), name + PROFILE_FILE_EXTENSION);
        }

        private string GetActiveProfilePointerPath()
        {
            return Path.Combine(this.GetConfigDirectory(), ACTIVE_PROFILE_POINTER_NAME);
        }

        private string ReadActiveProfilePointer()
        {
            string path = this.GetActiveProfilePointerPath();
            try
            {
                return File.Exists(path) ? File.ReadAllText(path).Trim() : null;
            }
            catch (IOException)
            {
                return null;
            }
        }

        private void WriteActiveProfilePointer(string name)
        {
            try
            {
                File.WriteAllText(this.GetActiveProfilePointerPath(), name);
            }
            catch (IOException)
            {
            }
        }

        // One-time lift of a single legacy config into Profiles\Default.json.
        private void MigrateToProfiles(string profilesDir)
        {
            if (Directory.GetFiles(profilesDir, "*" + PROFILE_FILE_EXTENSION).Any())
            {
                return;
            }

            string legacyName = string.IsNullOrEmpty(this._appConfig.ConfigFileName)
                ? LEGACY_CONFIG_FILE_NAME
                : this._appConfig.ConfigFileName;
            string legacyAppData = Path.Combine(this.GetConfigDirectory(), legacyName);
            string legacyExe = Path.Combine(AppContext.BaseDirectory, legacyName);
            string defaultPath = this.GetProfilePath(DEFAULT_PROFILE_NAME);

            try
            {
                if (File.Exists(legacyAppData))
                {
                    File.Copy(legacyAppData, defaultPath);
                }
                else if (File.Exists(legacyExe))
                {
                    File.Copy(legacyExe, defaultPath);
                }
                // else: no legacy file — Load() will create the default profile from defaults.
            }
            catch (IOException)
            {
            }
        }
        #endregion

        private static bool IsValidProfileName(string name)
        {
            return !string.IsNullOrWhiteSpace(name)
                && (name.IndexOfAny(Path.GetInvalidFileNameChars()) < 0);
        }

        private static string ComputeHash(string text)
        {
            using SHA256 sha = SHA256.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(text)));
        }

        public void Dispose()
        {
            _watcher?.Dispose();
            _debounceTimer?.Dispose();
        }
    }
}
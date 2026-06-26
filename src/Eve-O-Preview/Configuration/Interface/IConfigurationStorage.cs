using System;
using System.Collections.Generic;

namespace EveOPreview.Configuration
{
    public interface IConfigurationStorage
    {
        void Load();
        void Save();

        // Raised after an external edit to the active profile file is reloaded into the live
        // config. Fires on a background thread — subscribers must marshal to the UI thread
        // before re-registering hotkeys or touching WinForms controls (issue #94).
        event Action ConfigurationReloaded;

        // --- Profiles ---
        string ActiveProfileName { get; }
        IReadOnlyList<string> GetProfileNames();
        bool CreateProfile(string name);
        bool DeleteProfile(string name);
        bool SwitchProfile(string name);
        void ResetActiveProfileToDefaults();
        bool ExportProfile(string destinationPath);
        string ImportProfile(string sourcePath);
    }
}
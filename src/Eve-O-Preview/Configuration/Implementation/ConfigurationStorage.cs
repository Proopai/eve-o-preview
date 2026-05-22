using System.IO;
using Newtonsoft.Json;

namespace EveOPreview.Configuration.Implementation
{
	class ConfigurationStorage : IConfigurationStorage
	{
		private const string CONFIGURATION_FILE_NAME = "EVE-F-Preview.json";
		private const string LEGACY_CONFIGURATION_FILE_NAME = "EVE-O-Preview.json";

		private readonly IAppConfig _appConfig;
		private readonly IThumbnailConfiguration _thumbnailConfiguration;

		public ConfigurationStorage(IAppConfig appConfig, IThumbnailConfiguration thumbnailConfiguration)
		{
			this._appConfig = appConfig;
			this._thumbnailConfiguration = thumbnailConfiguration;
		}

		public void Load()
		{
			string filename = this.ResolveLoadConfigFileName();

			if (!File.Exists(filename))
			{
				return;
			}

			string rawData = File.ReadAllText(filename);

			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
			{
				ObjectCreationHandling = ObjectCreationHandling.Replace
			};

			JsonConvert.PopulateObject(rawData, this._thumbnailConfiguration, jsonSerializerSettings);

			this._thumbnailConfiguration.ApplyRestrictions();
		}

		public void Save()
		{
			string rawData = JsonConvert.SerializeObject(this._thumbnailConfiguration, Formatting.Indented);
			string filename = this.GetSaveConfigFileName();

			try
			{
				File.WriteAllText(filename, rawData);
			}
			catch (IOException)
			{
				// Ignore error if for some reason the updated config cannot be written down
			}
		}

		private string GetSaveConfigFileName()
		{
			return string.IsNullOrEmpty(this._appConfig.ConfigFileName)
				? ConfigurationStorage.CONFIGURATION_FILE_NAME
				: this._appConfig.ConfigFileName;
		}

		private string ResolveLoadConfigFileName()
		{
			if (!string.IsNullOrEmpty(this._appConfig.ConfigFileName))
			{
				return this._appConfig.ConfigFileName;
			}

			string newConfigPath = ConfigurationStorage.CONFIGURATION_FILE_NAME;
			if (File.Exists(newConfigPath))
			{
				return newConfigPath;
			}

			string legacyConfigPath = ConfigurationStorage.LEGACY_CONFIGURATION_FILE_NAME;
			if (File.Exists(legacyConfigPath))
			{
				return legacyConfigPath;
			}

			return newConfigPath;
		}
	}
}

using ESFA.DC.ILR.Desktop.Service.Interface;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class ConfigServiceStub : IConfigService
    {
        public ConfigServiceStub()
        {
            LoadConfigAppSettings();
        }

        public IDictionary<string, string> UserSettingsKeyValuePair { get; private set; }

        public void LoadConfigAppSettings()
        {
            UserSettingsKeyValuePair = new Dictionary<string, string>();
            var appSettings = ConfigurationManager.AppSettings;
                        
            foreach (string configKey in appSettings.AllKeys)
            {
                string key = configKey;
                string value = $@"{appSettings[configKey]}";
                UserSettingsKeyValuePair.Add(key, value);
            }
        }

        public bool SaveConfigAppSettings(string key, string value)
        {
            bool success = false;
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove(key);
                config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Full);
            }
            catch (System.Exception)
            {
                throw;
            }
            return success;
        }
    }
}

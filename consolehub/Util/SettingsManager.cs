using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolehub.Util
{
    static class SettingsManager
    {
        /// <summary>
        /// Configuration used to save once we've modified the settings.
        /// </summary>
        static Configuration appConfiguration;

        /// <summary>
        /// Collection in which our settings are saved.
        /// </summary>
        static KeyValueConfigurationCollection appSettings;

        /// <summary>
        /// Initializes and loads the default app configuration to use.
        /// </summary>
        public static void Init()
        {
            var currentAppLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            appConfiguration = ConfigurationManager.OpenExeConfiguration(currentAppLocation);
            appSettings = appConfiguration.AppSettings.Settings;
        }

        /// <summary>
        /// Returns whether there's a setting with the specified key
        /// </summary>
        /// <param name="key">Key to search in the settings</param>
        /// <returns>True if the key exists, False otherwise</returns>
        public static bool Exists(String key)
        {
            var setting = appSettings[key];
            return setting != null && !String.IsNullOrEmpty(setting.Value);
        }

        /// <summary>
        /// Returns the specified key if it exists.
        /// </summary>
        /// <param name="key">Key to search in the settings</param>
        /// <returns>Value of the specified key</returns>
        public static String Get(String key)
        {
            return appSettings[key].Value;
        }

        /// <summary>
        /// Removes the specified key if it already exists and adds the new given value.
        /// </summary>
        /// <param name="key">Key to add to the settings</param>
        /// <param name="value">Value linked to this key</param>
        public static void Set(String key, String value)
        {
            appSettings.Remove(key);
            appSettings.Add(key, value);
            appConfiguration.Save(ConfigurationSaveMode.Minimal);
        }

        /// <summary>
        /// Removes the specified key from the app settings.
        /// </summary>
        /// <param name="key">Key to remove</param>
        public static void Remove(string key)
        {
            appSettings.Remove(key);
            appConfiguration.Save(ConfigurationSaveMode.Minimal);
        }
    }
}

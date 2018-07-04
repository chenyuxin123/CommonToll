using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonToll.Toll
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public static class AppConfigHelper
    {
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Read(string key)
        {
            var setting = ConfigurationManager.AppSettings[key];
            return setting ?? string.Empty;
        }

        /// <summary>
        /// 读取指定类型配置数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Read<T>(string key)
        {
            var setting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(setting))
                return default(T);
            return (T)Convert.ChangeType(setting, typeof(T));
        }

        /// <summary>
        /// 读取配置(没有读取到则返回默认值)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T Read<T>(string key, T defaultValue)
        {
            var setting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(setting))
                return defaultValue;
            return (T)Convert.ChangeType(setting, typeof(T));
        }

        /// <summary>
        /// 写入配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Write<T>(string key, T value)
        {
            var setting = value != null ? value.ToString() : string.Empty;
            return Write(key, setting);
        }

        /// <summary>
        /// 写入配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Write(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return Write(key, value, config);
        }

        /// <summary>
        /// 写入配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="config">自定义配置文件</param>
        /// <returns></returns>
        public static bool Write<T>(string key, T value, Configuration config)
        {
            var setting = value != null ? value.ToString() : string.Empty;
            return Write(key, setting, config);
        }

        /// <summary>
        /// 写入配置信息
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="config">自定义配置文件</param>
        public static bool Write(string key, string value, Configuration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (config.AppSettings.Settings[key] != null)
                config.AppSettings.Settings[key].Value = value;
            else
                config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            return true;
        }

        /// <summary>
        /// 读取指定配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string Read(string key, Configuration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            var setting = config.AppSettings.Settings[key];
            if (setting == null || string.IsNullOrEmpty(setting.Value))
                return string.Empty;

            return setting.Value;
        }

        /// <summary>
        /// 读取指定配置文件 带默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="config"></param>
        public static T Read<T>(string key, T defaultValue, Configuration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            var setting = config.AppSettings.Settings[key];
            if (setting == null || string.IsNullOrEmpty(setting.Value))
                return defaultValue;

            return (T)Convert.ChangeType(setting.Value, typeof(T));
        }

        /// <summary>
        /// 根据文件路径加载配置文件
        /// </summary>
        /// <param name="configPath">文件路径</param>
        /// <returns></returns>
        public static Configuration Load(string configPath)
        {
            var map = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
            return ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string ConfigPath()
        {
            return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
        }
    }
}

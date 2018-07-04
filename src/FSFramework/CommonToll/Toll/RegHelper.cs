using Microsoft.Win32; //对注册表操作

namespace CommonToll.Toll
{
    /// <summary>
    /// 注册表操作帮助类
    /// </summary>
    public static class RegHelper
    {
        /// <summary>
        /// 读取路径为keyPath，键名为keyName的注册表键值，缺省返回def
        /// </summary>
        /// <param name="rootkey"></param>
        /// <param name="keyPath">路径</param>
        /// <param name="keyName">键名</param>
        /// <param name="rtn">默认为null</param>
        /// <returns></returns>        
        public static bool GetRegVal(RegistryKey rootkey, string keyPath, string keyName, out string rtn)
        {
            rtn = "";
            try
            {
                RegistryKey key = rootkey.OpenSubKey(keyPath);
                rtn = key.GetValue(keyName).ToString();
                key.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置路径为keyPath，键名为keyName的注册表键值为keyValue
        /// </summary>
        /// <param name="rootkey"></param>
        /// <param name="keyPath"></param>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public static bool SetRegVal(RegistryKey rootkey, string keyPath, string keyName, string keyValue)
        {
            try
            {
                RegistryKey key = rootkey.OpenSubKey(keyPath, true);
                if (key == null)
                    key = rootkey.CreateSubKey(keyPath);
                key.SetValue(keyName, (object)keyValue);
                key.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建路径为keyPath的键
        /// </summary>
        /// <param name="rootkey"></param>
        /// <param name="keyPath"></param>
        /// <returns></returns>
        private static RegistryKey CreateRegKey(RegistryKey rootkey, string keyPath)
        {
            try
            {
                return rootkey.CreateSubKey(keyPath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 删除路径为keyPath的子项
        /// </summary>
        /// <param name="rootkey"></param>
        /// <param name="keyPath"></param>
        /// <returns></returns>
        private static bool DelRegSubKey(RegistryKey rootkey, string keyPath)
        {
            try
            {
                rootkey.DeleteSubKey(keyPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除路径为keyPath的子项及其附属子项
        /// </summary>
        /// <param name="rootkey"></param>
        /// <param name="keyPath"></param>
        /// <returns></returns>
        private static bool DelRegSubKeyTree(RegistryKey rootkey, string keyPath)
        {
            try
            {
                rootkey.DeleteSubKeyTree(keyPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除路径为keyPath下键名为keyName的键值
        /// </summary>
        /// <param name="rootkey"></param>
        /// <param name="keyPath"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        private static bool DelRegKeyVal(RegistryKey rootkey, string keyPath, string keyName)
        {
            try
            {
                RegistryKey key = rootkey.OpenSubKey(keyPath, true);
                key.DeleteValue(keyName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
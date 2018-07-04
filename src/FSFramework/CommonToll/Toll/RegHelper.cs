using Microsoft.Win32; //��ע������

namespace CommonToll.Toll
{
    /// <summary>
    /// ע������������
    /// </summary>
    public static class RegHelper
    {
        /// <summary>
        /// ��ȡ·��ΪkeyPath������ΪkeyName��ע����ֵ��ȱʡ����def
        /// </summary>
        /// <param name="rootkey"></param>
        /// <param name="keyPath">·��</param>
        /// <param name="keyName">����</param>
        /// <param name="rtn">Ĭ��Ϊnull</param>
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
        /// ����·��ΪkeyPath������ΪkeyName��ע����ֵΪkeyValue
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
        /// ����·��ΪkeyPath�ļ�
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
        /// ɾ��·��ΪkeyPath������
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
        /// ɾ��·��ΪkeyPath������丽������
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
        /// ɾ��·��ΪkeyPath�¼���ΪkeyName�ļ�ֵ
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
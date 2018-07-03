using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace FSFramework.Toll
{
    /// <summary>
    /// 加解密帮助类
    /// </summary>
    public static class EncryptHelper
    {
        /// <summary>
        /// 3DES加密CBC模式(.NET封装的DES算法的默认模式)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] TripleDESEncodeCBC(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                MemoryStream mStream = new MemoryStream();
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;
                //tdsp.Padding = PaddingMode.PKCS7;
                CryptoStream cStream = new CryptoStream(mStream, tdsp.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                cStream.Write(data, 0, data.Length);
                cStream.FlushFinalBlock();
                byte[] ret = mStream.ToArray();
                cStream.Close();
                mStream.Close();
                return ret;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 3DES解密CBC模式(.NET封装的DES算法的默认模式)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] TripleDESDecodeCBC(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                MemoryStream msDecrypt = new MemoryStream(data);
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;
                //tdsp.Padding = PaddingMode.PKCS7;
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, tdsp.CreateDecryptor(key, iv), CryptoStreamMode.Read);

                byte[] fromEncrypt = new byte[data.Length];
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                return fromEncrypt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 3DES加密ECB模式(JAVA封装的DES算法的默认模式)，支持弱密钥
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] TripleDESEncodeECB(byte[] key, byte[] data)
        {
            try
            {
                MemoryStream mStream = new MemoryStream();
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                //tdsp.Padding = PaddingMode.PKCS7;
                //tdsp.Key = key;

                Type t = Type.GetType("System.Security.Cryptography.CryptoAPITransformMode");
                object obj = t.GetField("Encrypt", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).GetValue(t);

                MethodInfo mi = tdsp.GetType().GetMethod("_NewEncryptor", BindingFlags.Instance | BindingFlags.NonPublic);
                ICryptoTransform desCrypt = (ICryptoTransform)mi.Invoke(tdsp, new object[] { key, CipherMode.ECB, null, 0, obj });

                CryptoStream cStream = new CryptoStream(mStream, desCrypt, CryptoStreamMode.Write);
                cStream.Write(data, 0, data.Length);
                cStream.FlushFinalBlock();
                byte[] ret = mStream.ToArray();
                cStream.Close();
                mStream.Close();
                return ret;//系统函数是默认24字节，用于3倍长DES算法，目前只用双倍16字节
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 3DES解密ECB模式(JAVA封装的DES算法的默认模式)，支持弱密钥
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] TripleDESDecodeECB(byte[] key, byte[] data)
        {
            try
            {
                MemoryStream msDecrypt = new MemoryStream(data);
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;
                //tdsp.Padding = PaddingMode.PKCS7;
                //tdsp.Key = key;
                Type t = Type.GetType("System.Security.Cryptography.CryptoAPITransformMode");
                object obj = t.GetField("Encrypt", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).GetValue(t);

                MethodInfo mi = tdsp.GetType().GetMethod("_NewEncryptor", BindingFlags.Instance | BindingFlags.NonPublic);
                ICryptoTransform desCrypt = (ICryptoTransform)mi.Invoke(tdsp, new object[] { key, CipherMode.ECB, null, 0, obj });

                CryptoStream csDecrypt = new CryptoStream(msDecrypt, desCrypt, CryptoStreamMode.Read);

                byte[] fromEncrypt = new byte[data.Length];
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                return fromEncrypt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="data">待加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">加密矢量：只有在CBC解密模式下才适用</param>
        /// <param name="mode">运算模式</param>
        /// <returns>加密后的字符串</returns>
        public static string TripleDESEncode(string key, string iv, string data, CipherMode mode = CipherMode.ECB)
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = Convert.FromBase64String(key),
                    Mode = mode
                };
                if (mode == CipherMode.CBC)
                {
                    des.IV = Convert.FromBase64String(iv);
                }
                var desEncrypt = des.CreateEncryptor();
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="data">加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">解密矢量：只有在CBC解密模式下才适用</param>
        /// <param name="mode">运算模式</param>
        /// <returns>解密的字符串</returns>
        public static string TripleDESDecode(string key, string iv, string data, CipherMode mode = CipherMode.ECB)
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = Convert.FromBase64String(key),
                    Mode = mode,
                    Padding = PaddingMode.PKCS7
                };
                if (mode == CipherMode.CBC)
                {
                    des.IV = Convert.FromBase64String(iv);
                }
                var desDecrypt = des.CreateDecryptor();
                var result = "";
                byte[] buffer = Convert.FromBase64String(data);
                result = Encoding.UTF8.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// SHA1哈希算法(UTF-8编码)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Sha1Encode(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] buffer = SHA1.Create().ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// MD5哈希算法(UTF-8编码)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5Encode(string data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] buffer = md5.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                sb.Append(buffer[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}

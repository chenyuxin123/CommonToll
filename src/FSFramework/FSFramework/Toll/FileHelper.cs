using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSFramework.Toll
{
    public static class FileHelper
    {
        /// <summary>
        /// 比较文件哈希值
        /// </summary>
        /// <param name="filePath1"></param>
        /// <param name="filePath2"></param>
        /// <returns></returns>
        public static bool CompareFileHash(string filePath1, string filePath2)
        {
            string p_1 = filePath1;
            string p_2 = filePath2;

            //计算第一个文件的哈希值
            var hash = System.Security.Cryptography.HashAlgorithm.Create();
            var stream_1 = new FileStream(p_1, FileMode.Open);
            byte[] hashByte_1 = hash.ComputeHash(stream_1);
            stream_1.Close();
            //计算第二个文件的哈希值
            var stream_2 = new FileStream(p_2, FileMode.Open);
            byte[] hashByte_2 = hash.ComputeHash(stream_2);
            stream_2.Close();

            //比较两个哈希值
            return BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2);
        }
    }
}

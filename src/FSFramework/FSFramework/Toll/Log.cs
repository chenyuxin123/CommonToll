using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSFramework.Toll
{
    /// <summary>
    /// 写日志
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog(string msg)
        {
            string filePath = "Log\\";
            WriteLog(filePath, msg);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="filePath">日志存放路径</param>
        /// <param name="log"></param>
        public static void WriteLog(string filePath, string log)
        {
            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                filePath += DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                StreamWriter sw = new StreamWriter(filePath, true, Encoding.Default);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "> " + log + "\r\n");
                sw.Close();
            }
            catch
            { }
        }
    }
}

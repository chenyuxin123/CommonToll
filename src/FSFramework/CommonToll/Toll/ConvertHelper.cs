using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonToll.Toll
{
    /// <summary>
    /// 数据转换帮助类
    /// </summary>
    public static class ConvertHelper
    {
        //ASCII	    1个字节 
        //Unicode	2个字节，生僻字4个字节
        //UTF-8	    1-6个字节，英文字母1个字节，汉字3个字节，生僻字4-6个字节

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHexString(this byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 16进制字符串转字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(this string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return returnBytes;
        }

        /// <summary>
        /// Base64字符转字节数组（图像）
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static byte[] Base64StringToBytes(this string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        /// <summary>
        /// 转大写人民币格式
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToRmbUpper(this decimal number)
        {
            var s = number.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            var r = Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return r;
        }

        /// <summary>
        /// 转人民币格式
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToRmbLower(this decimal number)
        {
            return number.ToString("C");
        }

        /// <summary>
        /// 保留位数
        /// </summary>
        /// <param name="number"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        //public static string Hold(this double number,ushort digit)
        //{
        //    return string.Format("{0:N" + digit + "}", number);
        //}
            
    }
}

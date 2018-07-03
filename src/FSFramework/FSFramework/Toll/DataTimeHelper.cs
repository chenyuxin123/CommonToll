using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FSFramework.Toll
{
    /// <summary>
    /// 
    /// </summary>
    public class DataTimeHelper
    {
        /// <summary>
        /// Unix时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public DateTime UnixTimeToDateTime(string timeStamp)
        {
            //处理字符串,截取括号内的数字
            var strStamp = Regex.Matches(timeStamp, @"(?<=\()((?<gp>\()|(?<-gp>\))|[^()]+)*(?(gp)(?!))").Cast<Match>().Select(t => t.Value).ToArray()[0].ToString();
            //处理字符串获取+号前面的数字
            var str = Convert.ToInt64(strStamp.Substring(0, strStamp.IndexOf("+")));
            long timeTricks = new DateTime(1970, 1, 1).Ticks + str * 10000 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
            return new DateTime(timeTricks);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public int DateTimeToUnixTime(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
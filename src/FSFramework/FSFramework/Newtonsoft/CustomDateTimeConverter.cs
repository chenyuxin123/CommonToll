using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FSFramework.Newtonsoft
{
    /// <summary>
    /// 自定义Json日期转换器(yyyy-MM-dd HH:mm:ss)
    /// </summary>
    public class CustomDateTimeConverter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter isoDataTimeConverter = new IsoDateTimeConverter();

        static CustomDateTimeConverter()
        {
            isoDataTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return isoDataTimeConverter.ReadJson(reader, objectType, existingValue, serializer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            isoDataTimeConverter.WriteJson(writer, value, serializer);
        }
    }
}

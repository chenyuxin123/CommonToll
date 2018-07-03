using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FSFramework.Toll
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// 要复制的实例必须可序列化，包括实例引用的其它实例都必须在类定义时加[Serializable]特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="realObject"></param>
        /// <returns></returns>
        public static T Copy<T>(this T realObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制     
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, realObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }
    }
}

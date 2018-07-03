using System;
using System.Reflection;

namespace FSFramework.Toll
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 获取私有字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        public static T GetPrivateFieldValue<T>(this object instance, string fieldname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            return (T)field.GetValue(instance);
        }

        /// <summary>
        /// 获取私有属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public static T GetPrivatePropertyValue<T>(this object instance, string propertyname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(propertyname, flag);
            return (T)property.GetValue(instance);
        }

        /// <summary>
        /// 设置私有字段的值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="fieldname"></param>
        /// <param name="value"></param>
        public static void SetPrivateFieldValue(this object instance, string fieldname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            field.SetValue(instance, value);
        }

        /// <summary>
        /// 设置私有属性的值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyname"></param>
        /// <param name="value"></param>
        public static void SetPrivatePropertyValue(this object instance, string propertyname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(propertyname, flag);
            property.SetValue(instance, value);
        }

        /// <summary>
        /// 执行私有方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T InvokePrivateMethod<T>(this object instance, string name, params object[] param)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            MethodInfo method = type.GetMethod(name, flag);
            return (T)method.Invoke(instance, param);
        }

        /// <summary>
        /// 获取公有属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public static T GetPublicPropertyValue<T>(this object instance, string propertyname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(propertyname, flag);
            return (T)property.GetValue(instance);
        }

        /// <summary>
        /// 设置公有属性的值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyname"></param>
        /// <param name="value"></param>
        public static void SetPublicPropertyValue(this object instance, string propertyname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(propertyname, flag);
            property.SetValue(instance, value);
        }

        /// <summary>
        /// 执行公有方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T InvokePublicMethod<T>(this object instance, string name, params object[] param)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public;
            Type type = instance.GetType();
            MethodInfo method = type.GetMethod(name, flag);
            return (T)method.Invoke(instance, param);
        }
    }
}

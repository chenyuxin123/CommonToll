using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonToll.Toll
{
    [AttributeUsage(AttributeTargets.Constructor |
        AttributeTargets.Property |
        AttributeTargets.Method,
        AllowMultiple = false)]
    public class InjectionAttribute : Attribute
    {

    }

    public class ServiceContainer
    {
        private readonly ConcurrentDictionary<Type, Type> serviceMap = new ConcurrentDictionary<Type, Type>();

        public void Register(Type from, Type to)
        {
            serviceMap[from] = to;
        }

        public object GetService(Type serviceType)
        {
            Type type;
            if (!serviceMap.TryGetValue(serviceType, out type))
            {
                type = serviceType;
            }
            if (type.IsInterface || type.IsAbstract)
            {
                return null;
            }

            ConstructorInfo constructorInfo = GetConstructor(type);
            if (constructorInfo == null)
            {
                return null;
            }

            object[] arguments = constructorInfo.GetParameters().Select(x => GetService(x.ParameterType)).ToArray();
            object service = constructorInfo.Invoke(arguments);
            InitProperties(service);
            InvokeMethods(service);
            return service;
        }

        protected virtual ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            return constructorInfos.FirstOrDefault(x => x.GetCustomAttribute<InjectionAttribute>() != null) ??
                constructorInfos.FirstOrDefault();
        }

        protected virtual void InitProperties(object service)
        {
            PropertyInfo[] properties = service.GetType().GetProperties()
                .Where(x => x.CanWrite && x.GetCustomAttribute<InjectionAttribute>() != null)
                .ToArray();
            Array.ForEach(properties, p => p.SetValue(service, GetService(p.PropertyType)));
        }

        protected virtual void InvokeMethods(object service)
        {
            MethodInfo[] methods = service.GetType().GetMethods()
                .Where(m => m.GetCustomAttribute<InjectionAttribute>() != null)
                .ToArray();
            Array.ForEach(methods, m =>
            {
                object[] arguments = m.GetParameters().Select(p => GetService(p.ParameterType)).ToArray();
                m.Invoke(service, arguments);
            });
        }
    }
}

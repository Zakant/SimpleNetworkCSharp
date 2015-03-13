using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimpleNetwork.Package.Extensions
{
    internal static class ReflectionExtensions
    {
        private static BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public static void ReplaceValues<T>(this T o, Type attributeType, object value) where T : class
        {
            ReplaceValues(o, attributeType, x => value);
        }

        public static void ReplaceValues<T>(this T o, Type attributeType, Func<PropertyInfo, object> valueFunction) where T : class
        {
            var type = o.GetType();
            var properties = type.GetProperties(flags);

            foreach (var p in properties)
                if (Attribute.IsDefined(p, attributeType))
                    p.SetValue(o, valueFunction(p), null);
        }

        public static void ReplaceAllWithDefault<T>(this T o, Type attributeType) where T : class
        {
            ReplaceValues(o, attributeType, x => GetDefault(x.PropertyType));
        }


        private static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}

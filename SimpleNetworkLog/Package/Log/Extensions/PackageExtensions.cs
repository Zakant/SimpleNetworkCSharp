using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    /// <summary>
    /// Stellt Erweiterungs Methoden für Pakete bereit.
    /// </summary>
    public static class PackageExtensions
    {
        /// <summary>
        /// Stellt fest, ob ein Paket im Log auftauchen soll.
        /// </summary>
        /// <param name="package">Das zu pruefende Paket.</param>
        /// <returns>Ein Wahrheitswert, der angibt, ob das Paket im Log auftauchen soll.</returns>
        public static bool shouldLogged(this IPackage package)
        {
            return !Attribute.IsDefined(package.GetType(), typeof(NonLoggedAttribute));
        }

        private static BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        internal static IPackage hideFields(this IPackage package)
        {
            var type = package.GetType();

            var fields = type.GetFields(flags);
            var properties = type.GetProperties(flags);

            foreach (var f in fields)
                if (Attribute.IsDefined(f, typeof(HideInLogAttribute)))
                    f.SetValue(package, GetDefault(f.FieldType));
            foreach (var p in properties)
                if (Attribute.IsDefined(p, typeof(HideInLogAttribute)))
                    p.SetValue(package, GetDefault(p.PropertyType), null);

            return package;
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

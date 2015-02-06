using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SimpleNetwork.Package
{
    public static class PackageExtensions
    {
        public static IPackage Clone(this IPackage package)
        {
            if (package is ICloneable) return (package as ICloneable).Clone() as IPackage;
            return package.toByteArray().toPackage();
        }

        public static byte[] toByteArray(this IPackage package)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, package);
                return ms.ToArray();
            }
        }

        public static IPackage toPackage(this byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(ms) as IPackage;
            }
        }

    }
}

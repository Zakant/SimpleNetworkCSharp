using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SimpleNetwork.Utils
{
    public static class PackageExtensions
    {
        public static byte[] ToByteArray(this IPackage package)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using(MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, package);
                return ms.ToArray();
            }
        }

        public static IPackage ToPackage(this byte[] packagedata)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(packagedata))
                return bf.Deserialize(ms) as IPackage;
        }

    }
}

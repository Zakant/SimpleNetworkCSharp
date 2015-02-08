using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SimpleNetwork.Package
{
    /// <summary>
    /// Stellt Erweiterungs Methoden fuer Pakete bereit.
    /// </summary>
    public static class PackageExtensions
    {
        /// <summary>
        /// Erstellt eine Kopie eines Paketes.
        /// </summary>
        /// <param name="package">Das zu kopierende Paket.</param>
        /// <returns>Die Kopie des urspruenglichen Paketes.</returns>
        public static IPackage Clone(this IPackage package)
        {
            if (package is ICloneable) return (package as ICloneable).Clone() as IPackage;
            return package.toByteArray().toPackage();
        }

        /// <summary>
        /// Wandelt ein Paket in ein Byte Array um.
        /// </summary>
        /// <param name="package">Das zu behandelnde Paket.</param>
        /// <returns>Die Byte Array Repraesentation des Paketes.</returns>
        public static byte[] toByteArray(this IPackage package)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, package);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Wandelt ein Byte Array in ein Paket um.
        /// </summary>
        /// <param name="data">Das Byte Array aus dem das Paket gelesen werden soll.</param>
        /// <returns>Das gelesene Paket.</returns>
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

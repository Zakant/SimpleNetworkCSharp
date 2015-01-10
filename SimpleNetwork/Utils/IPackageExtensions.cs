using SimpleNetwork.Package.Packages;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimpleNetwork.Utils
{
    /// <summary>
    /// Stellt statische Erweiterungsmethoden für alle Pakete bereit.
    /// </summary>
    public static class PackageExtensions
    {
        /// <summary>
        /// Gibt die Byte-Array Repräsentation eines Paketes zurück.
        /// </summary>
        /// <param name="package">Das zuverarbeitende Paket.</param>
        /// <returns>Die Byte-Array Repräsentation.</returns>
        public static byte[] ToByteArray(this IPackage package)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using(MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, package);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Gibt das durch das Byte-Array dargestellte Paket zurück.
        /// </summary>
        /// <param name="packagedata">Das Byte-Array, aus dem das Paket erzeugt werden soll.</param>
        /// <returns>Das erzeugte Paket.</returns>
        public static IPackage ToPackage(this byte[] packagedata)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(packagedata))
                return bf.Deserialize(ms) as IPackage;
        }

    }
}

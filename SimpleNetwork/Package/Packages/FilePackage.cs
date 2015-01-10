using System;
using System.IO;

namespace SimpleNetwork.Package.Packages
{
    /// <summary>
    /// Stellt ein Packet da, um Datein zu übertragen.
    /// </summary>
    [Serializable]
    public class FilePackage : IPackage
    {
        /// <summary>
        /// Die Bytefolge, durch die die Datei dargestellt.
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// Der Name der Datei.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Das FileInfo-Objekt der Datei.
        /// </summary>
        public FileInfo Info { get; set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der FilePackage-Klasse unter verwendung des angegeben FileInfo-Objekt.
        /// </summary>
        /// <param name="fi">Das FileInfo-Objekt.</param>
        public FilePackage(FileInfo fi)
        {
            if (!fi.Exists) throw new Exception(String.Format("File \"{0}\" does not exsits!", fi.FullName));
            Data = File.ReadAllBytes(fi.FullName);
            Name = fi.Name;
            Info = fi;
        }

    }
}

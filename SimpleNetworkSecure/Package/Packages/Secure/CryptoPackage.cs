using System;

namespace SimpleNetwork.Package.Packages.Secure
{
    /// <summary>
    /// Ein Paket, das ein verschlüsseltes Paket transportiert.
    /// </summary>
    [Serializable]
    public class CryptoPackage : IPackage
    {
        /// <summary>
        /// Das Verschlüsselte Paket.
        /// </summary>
        public byte[] Data { get; protected set; }

        /// <summary>
        /// Erstellt ein neues Paket um ein verschlüsseltes Paket zu transportieren.
        /// </summary>
        /// <param name="data">Das verschlüsselte Paket.</param>
        public CryptoPackage(byte[] data)
        { Data = data; }

    }
}

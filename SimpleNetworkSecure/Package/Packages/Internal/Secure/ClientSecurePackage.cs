using System;

namespace SimpleNetwork.Package.Packages.Internal.Secure
{
    /// <summary>
    /// Ein Paket das an einen Server gesendet wird, um den Aufbau einer sicheren Verbindung abzuschließen.
    /// </summary>
    [Serializable]
    public class ClientSecurePackage : IPackage
    {
        /// <summary>
        /// Der öffentliche Schlüssel für den Verbindungsabschluß.
        /// </summary>
        public byte[] PublicKey { get; protected set; }

        /// <summary>
        /// Erstellt ein neues Paket zum Abschluß einer sicheren Verbindung.
        /// </summary>
        /// <param name="publickey">Der öffentliche Schlüssel für den Verbindungsabschluß.</param>
        public ClientSecurePackage(byte[] publickey)
        {
            PublicKey = publickey;
        }
    }
}

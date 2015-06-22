using System;


namespace SimpleNetwork.Package.Packages.Internal.Secure
{
    /// <summary>
    /// Ein Paket das an einen Client gesendet wird, um den Aufbau einer sicheren Verbindung zu beginnen.
    /// </summary>
    [Serializable]
    public class ServerSecurePackage : IPackage
    {
        /// <summary>
        /// Der öffentliche Schlüssel für den Verbindungsaufbau.
        /// </summary>
        public byte[] PublicKey { get; protected set; }

        /// <summary>
        /// Der Initialisierungsvektor für den Verbindungsaufbau.
        /// </summary>
        public byte[] IV { get; protected set; }

        /// <summary>
        /// Erstellt ein neues Paket zum Aufbau einer sicheren Verbindung.
        /// </summary>
        /// <param name="publickey">Der öffentliche Schlüssel für den Verbindungsaufbau.</param>
        /// <param name="iv">Der Initialisierungsvektor für den Verbindungsaufbau.</param>
        public ServerSecurePackage(byte[] publickey,byte[] iv)
        {
            PublicKey = publickey;
            IV = iv;
        }
    }
}

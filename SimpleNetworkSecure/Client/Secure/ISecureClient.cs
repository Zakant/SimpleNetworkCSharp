using SimpleNetwork.Events.Secure;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Packages.Secure;
using System;

namespace SimpleNetwork.Client.Secure
{
    /// <summary>
    /// Stellt einen Vertrag da, um sicher mit einem Remotehost zu kommunizieren.
    /// </summary>
    public interface ISecureClient : ISecureClient
    {
        /// <summary>
        /// Tritt ein, wenn sich der Zustand der sicheren Verbindung veränert.
        /// </summary>
        event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;

        /// <summary>
        /// Der aktuelle Zustand der Verbindung.
        /// </summary>
        ConnectionState State { get; }

        /// <summary>
        /// Der öffentliche Schlüssel der Verbindung.
        /// </summary>
        byte[] PublicKey { get; }

        /// <summary>
        /// Der geheime Schlüssel der Verbindung
        /// </summary>
        byte[] SharedKey { get; }
    }
}

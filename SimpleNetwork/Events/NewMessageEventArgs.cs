using SimpleNetwork.Client;
using SimpleNetwork.Package.Packages;
using System;

namespace SimpleNetwork.Events
{
    /// <summary>
    /// Stellt Daten für das <see cref="SimpleNetwork.Package.Provider.IPackageProvider.NewMessage"/> Ereigniss bereit.
    /// </summary>
    public class NewMessageEventArgs : SimpleNetworkEventArgs
    {
        /// <summary>
        /// Der Remotehost, von dem das Packet stammt.
        /// </summary>
        public IClient Client { get; protected set; }
        /// <summary>
        /// Das empfangene Packet.
        /// </summary>
        public IPackage Package { get; protected set; }
        /// <summary>
        /// Der Typ des empfangenen Packetes.
        /// </summary>
        public Type MessageType { get { return (this.Package.GetType()); } }

        /// <summary>
        /// Initialisiert eine neue Instanz der NewMessageEventArgs unter verwendung des angegeben IClient-Objekts und dem IPackage-Objekt.
        /// </summary>
        /// <param name="client">Der IClient, von dem die Nachricht stammt.</param>
        /// <param name="package">Das empfangene Packet.</param>
        public NewMessageEventArgs(IClient client, IPackage package)
        {
            Client = client;
            Package = package;
        }
    }
}

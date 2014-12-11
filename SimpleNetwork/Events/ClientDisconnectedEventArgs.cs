using SimpleNetwork.Client;

namespace SimpleNetwork.Events
{
    /// <summary>
    /// Stellt Daten für das <see cref="SimpleNetwork.Server.IServer.ClientDisconnected"/> Ereigniss bereit.
    /// </summary>
    public class ClientDisconnectedEventArgs : SimpleNetworkEventArgs
    {
        /// <summary>
        /// Der Grund für den Verbindungsabbruch mit dem Remotehost.
        /// </summary>
        public DisconnectReason Reason { get; protected set; }
        
        /// <summary>
        /// Der Remotehost.
        /// </summary>
        public IClient Client { get; protected set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der HostFoundEventArgs unter verwendung des angegeben HostData-Objekts und des Grundes .
        /// </summary>
        /// <param name="c">Der Remotehost</param>
        /// <param name="r">Der Grund</param>
        public ClientDisconnectedEventArgs(IClient c, DisconnectReason r)
        {
            Client = c;
            Reason = r;
        }
    }
}

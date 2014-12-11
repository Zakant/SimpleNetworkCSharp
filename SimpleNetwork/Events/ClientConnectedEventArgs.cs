using SimpleNetwork.Client;

namespace SimpleNetwork.Events
{
    /// <summary>
    /// Stellt Daten für das <see cref="SimpleNetwork.Server.IServer.ClientConnected"/> Ereigniss bereit.
    /// </summary>
    public class ClientConnectedEventArgs : SimpleNetworkEventArgs
    {
        /// <summary>
        /// Der neu verbundene Client.
        /// </summary>
        public IClient Client { get; protected set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der ClientConnectedEventArgs unter verwendung des angegeben ICLient-Objekts.
        /// </summary>
        /// <param name="client">Der neu verbunende IClient.</param>
        public ClientConnectedEventArgs(IClient client)
        {
            Client = client;
        }
    }
}

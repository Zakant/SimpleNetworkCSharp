using SimpleNetwork.Client.Secure;

namespace SimpleNetwork.Events.Secure
{
    /// <summary>
    /// Stellt Daten für das  <see cref="SimpleNetwork.Client.Secure.ISecureClient.ConnectionStateChanged"/> Ereignis bereit.
    /// </summary>
    public class ConnectionStateChangedEventArgs : SimpleNetworkEventArgs
    {
        /// <summary>
        /// Der alte Verbindungszustand.
        /// </summary>
        public ConnectionState OldState { get; protected set; }
        /// <summary>
        /// Der neue Verbindungszustand.
        /// </summary>
        public ConnectionState NewState { get; protected set; }

        /// <summary>
        /// Erstellt ein 
        /// </summary>
        /// <param name="Old"></param>
        /// <param name="New"></param>
        public ConnectionStateChangedEventArgs(ConnectionState Old, ConnectionState New)
        {
            OldState = Old;
            NewState = New;
        }
    }
}

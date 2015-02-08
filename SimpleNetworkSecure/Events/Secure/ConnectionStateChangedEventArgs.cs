using SimpleNetwork.Client.Secure;

namespace SimpleNetwork.Events.Secure
{
    /// <summary>
    /// Stellt Daten für das  
    /// </summary>
    public class ConnectionStateChangedEventArgs : SimpleNetworkEventArgs
    {
        public ConnectionState OldState { get; protected set; }
        public ConnectionState NewState { get; protected set; }

        public ConnectionStateChangedEventArgs(ConnectionState Old, ConnectionState New)
        {
            OldState = Old;
            NewState = New;
        }
    }
}

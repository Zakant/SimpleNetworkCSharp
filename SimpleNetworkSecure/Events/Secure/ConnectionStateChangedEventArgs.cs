using SimpleNetwork.Client.Secure;

namespace SimpleNetwork.Events.Secure
{
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

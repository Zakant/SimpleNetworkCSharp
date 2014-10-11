using SimpleNetwork.Client.Secure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

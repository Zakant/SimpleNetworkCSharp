using SimpleNetwork.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Events
{
    public class ClientDisconnectedEventArgs : SimpleNetworkEventArgs
    {
        public DisconnectReason Reason { get; protected set; }
        public IClient Client { get; protected set; }

        public ClientDisconnectedEventArgs(IClient c, DisconnectReason r)
        {
            Client = c;
            Reason = r;
        }
    }
}

using SimpleNetwork.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Events
{
    public class ClientConnectedEventArgs : SimpleNetworkEventArgs
    {
        public IClient Client { get; protected set; }

        public ClientConnectedEventArgs(IClient client)
        {
            Client = client;
        }
    }
}

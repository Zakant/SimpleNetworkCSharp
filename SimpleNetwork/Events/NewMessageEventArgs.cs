using SimpleNetwork.Client;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Events
{
    public class NewMessageEventArgs : SimpleNetworkEventArgs
    {
        public IClient Client { get; protected set; }
        public IPackage Package { get; protected set; }
        public Type MessageType { get { return (this.Package.GetType()); } }

        public NewMessageEventArgs(IClient client, IPackage package)
        {
            Client = client;
            Package = package;
        }
    }
}

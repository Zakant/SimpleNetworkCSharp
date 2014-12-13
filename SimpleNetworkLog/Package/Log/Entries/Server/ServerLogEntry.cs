using SimpleNetwork.Client;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Server
{
    public class ServerLogEntry : IServerLogEntry
    {
        public IServer Server { get; protected set; }

        public IClient Client { get; protected set; }

        public IPackageLog Log { get; protected set; }

        public ServerLogEntry(IServer server, IClient client, IPackageLog log)
        {
            Server = server;
            Client = client;
            Log = log;
        }
    }
}

using SimpleNetwork.Client;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Server
{
    [Serializable]
    public class ServerLogEntry : IServerLogEntry
    {
        public IServer Server { get; protected set; }

        public IClient Client { get; protected set; }

        public IPackageLog Log { get; protected set; }

        public ServerLogEntry(IServer server, IClient client, IPackageLog log)
        {
            Contract.Requires<ArgumentNullException>(server != null);
            Contract.Requires<ArgumentNullException>(client != null);
            Contract.Requires<ArgumentNullException>(log != null);

            Server = server;
            Client = client;
            Log = log;
        }
    }
}

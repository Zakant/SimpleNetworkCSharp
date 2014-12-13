using SimpleNetwork.Client;
using SimpleNetwork.Package.Log.Entries;
using SimpleNetwork.Package.Log.Entries.Server;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    public class ServerPackageLog : IServerPackageLog
    {
        public IServer Server { get; protected set; }

        public bool keepOnDisconnect { get; set; }

        protected Dictionary<IClient, IPackageLog> clientLogs = new Dictionary<IClient, IPackageLog>();

        public IEnumerable<IServerLogEntry> Logs
        {
            get { return clientLogs.Select(x => new ServerLogEntry(Server, x.Key, x.Value) as IServerLogEntry); }
        }

        public ServerPackageLog(IServer server)
        {
            if (server == null) throw new ArgumentNullException("server");
            Server = server;
            server.ClientConnected += (s, e) =>
                {
                    clientLogs.Add(e.Client, e.Client.createLog());
                };
            server.ClientDisconnected += (s, e) =>
                {
                    if (!keepOnDisconnect)
                        clientLogs.Remove(e.Client);
                };
            keepOnDisconnect = true;
        }

        public IPackageLog getLogForClient(IClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (!clientLogs.ContainsKey(client)) throw new ArgumentException("There is no log available for this client in this sever log");
            return clientLogs[client];
        }

        public void addPackageLog(IPackageLog log)
        {
            if (log == null) throw new ArgumentNullException("log");
            if (clientLogs.ContainsKey(log.Client))
                clientLogs.Remove(log.Client);
            clientLogs.Add(log.Client, log);
        }
    }
}

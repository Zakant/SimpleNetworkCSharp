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
    /// <summary>
    /// Stellt ein Paket Log fuer einen <see cref="SimpleNetwork.Server.IServer"/> da.
    /// </summary>
    [Serializable]
    public class ServerPackageLog : IServerPackageLog
    {
        /// <summary>
        /// Der <see cref="SimpleNetwork.Server.IServer"/> fuer den das Log erstellt wurde.
        /// </summary>
        public IServer Server { get; protected set; }

        /// <summary>
        /// Gibt an, ob ein Client Log ueber den Disconnect des Clients gespeichert werden soll.
        /// </summary>
        public bool keepOnDisconnect { get; set; }

        /// <summary>
        /// Interne Sammlung der Client Logs.
        /// </summary>
        protected Dictionary<IClient, IPackageLog> clientLogs = new Dictionary<IClient, IPackageLog>();

        /// <summary>
        /// Eine Auflistung aller verfuegbaren Logs.
        /// </summary>
        public IEnumerable<IServerLogEntry> Logs
        {
            get { return clientLogs.Select(x => new ServerLogEntry(Server, x.Key, x.Value) as IServerLogEntry); }
        }

        /// <summary>
        /// Erstellt ein neues Log fuer den angegebenen Server.
        /// </summary>
        /// <param name="server">Der Server fuer den das Log erstellt werden soll.</param>
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

        /// <summary>
        /// Gibt das Log fuer einen mit dem Server verbundenen Client zurueck.
        /// </summary>
        /// <param name="client">Der Client fuer den das Log zurueckgegeben werden soll.</param>
        /// <returns>Das Log fuer den angegebenen Client.</returns>
        public IPackageLog getLogForClient(IClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (!clientLogs.ContainsKey(client)) throw new ArgumentException("There is no log available for this client in this sever log");
            return clientLogs[client];
        }

        /// <summary>
        /// Fuegt ein neues Log hinzu.
        /// </summary>
        /// <param name="log">Das hinzuzufuegende Log.</param>
        public void addPackageLog(IPackageLog log)
        {
            if (log == null) throw new ArgumentNullException("log");
            if (clientLogs.ContainsKey(log.Client))
                clientLogs.Remove(log.Client);
            clientLogs.Add(log.Client, log);
        }
    }
}

using SimpleNetwork.Client;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Server
{
    /// <summary>
    /// Stellt einen Eintrag in einem <see cref="SimpleNetwork.Package.Log.ServerPackageLog"/> da.
    /// </summary>
    [Serializable]
    public class ServerLogEntry : IServerLogEntry
    {
        /// <summary>
        /// Der <see cref="SimpleNetwork.Server.IServer"/> fuer den der Eintrag erstellt wurde.
        /// </summary>
        public IServer Server { get; protected set; }

        /// <summary>
        /// Der <see cref="SimpleNetwork.Client.IClient"/> fuer den der Eintrag erstellt wurde.
        /// </summary>
        public IClient Client { get; protected set; }

        /// <summary>
        /// Das zugehoerige <see cref="SimpleNetwork.Package.Log.IPackageLog"/>.
        /// </summary>
        public IPackageLog Log { get; protected set; }

        /// <summary>
        /// Erstellt einen Log Eintrag.
        /// </summary>
        /// <param name="server">Der <see cref="SimpleNetwork.Server.IServer"/> fuer den das Log erstellt wurde.</param>
        /// <param name="client">Der <see cref="SimpleNetwork.Client.IClient"/> fuer den der Eintrag erstellt wurde.</param>
        /// <param name="log">Das zugehoerige <see cref="SimpleNetwork.Package.Log.IPackageLog"/>.</param>
        public ServerLogEntry(IServer server, IClient client, IPackageLog log)
        {
            Server = server;
            Client = client;
            Log = log;
        }
    }
}

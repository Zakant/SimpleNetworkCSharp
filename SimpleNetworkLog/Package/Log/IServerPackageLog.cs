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
    /// Stellt ein Paket Log für einen <see cref="SimpleNetwork.Server.IServer"/> da.
    /// </summary>
    public interface IServerPackageLog
    {
        /// <summary>
        /// Der <see cref="SimpleNetwork.Server.IServer"/> für den das Log erstellt wurde.
        /// </summary>
        IServer Server { get; }

        /// <summary>
        /// Gibt an, ob ein Client Log ueber den Disconnect des Clients gespeichert werden soll.
        /// </summary>
        bool keepOnDisconnect { get; set; }

        /// <summary>
        /// Eine Auflistung aller verfuegbaren Logs.
        /// </summary>
        IEnumerable<IServerLogEntry> Logs { get; }

        /// <summary>
        /// Gibt das Log für einen mit dem Server verbundenen Client zurueck.
        /// </summary>
        /// <param name="client">Der Client für den das Log zurueckgegeben werden soll.</param>
        /// <returns>Das Log für den angegebenen Client.</returns>
        IPackageLog getLogForClient(IClient client);

        /// <summary>
        /// Fuegt ein neues Log hinzu.
        /// </summary>
        /// <param name="log">Das hinzuzufuegende Log.</param>
        void addPackageLog(IPackageLog log);
    }
}

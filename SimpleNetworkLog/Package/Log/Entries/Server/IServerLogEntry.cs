using SimpleNetwork.Client;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Server
{
    /// <summary>
    /// Stellt einen Eintrag in einem <see cref="SimpleNetwork.Package.Log.IServerPackageLog"/> da.
    /// </summary>
    public interface IServerLogEntry
    {
        /// <summary>
        /// Der <see cref="SimpleNetwork.Server.IServer"/> fuer den der Eintrag erstellt wurde.
        /// </summary>
        IServer Server { get; }

        /// <summary>
        /// Der <see cref="SimpleNetwork.Client.IClient"/> fuer den der Eintrag erstellt wurde.
        /// </summary>
        IClient Client { get; }

        /// <summary>
        /// Das zugehoerige <see cref="SimpleNetwork.Package.Log.IPackageLog"/>.
        /// </summary>
        IPackageLog Log { get; }
    }
}

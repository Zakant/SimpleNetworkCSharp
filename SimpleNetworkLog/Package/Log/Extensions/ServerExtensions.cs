using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    /// <summary>
    /// Stellt Erweiterungs Methoden fuer Server bereit.
    /// </summary>
    public static class ServerExtensions
    {
        /// <summary>
        /// Erstellt ein neues Log fuer den angegebenen Server.
        /// </summary>
        /// <param name="server">Der Server, fuer den das Log erstellt werden soll.</param>
        /// <returns>Das neu erstellte Log fuer den Server.</returns>
        public static IServerPackageLog createLog(this IServer server)
        {
            if (server == null) throw new ArgumentNullException("server");
            return new ServerPackageLog(server);
        }
    }
}

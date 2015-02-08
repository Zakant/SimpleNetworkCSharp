using SimpleNetwork.Client;
using SimpleNetwork.Package.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    /// <summary>
    /// Stellt Erweiterungsmethoden fuer Clients bereit.
    /// </summary>
    public static class ClientExtensions
    {
        /// <summary>
        /// Erstellt ein neues Log fuer den angegebenen Client.
        /// </summary>
        /// <param name="client">Der Client, fuer den das Log erstellt werden soll.</param>
        /// <returns>Das neu erstellte Log fuer den Client.</returns>
        public static IPackageLog createLog(this IClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            return new PackageLog(client);
        }

    }
}

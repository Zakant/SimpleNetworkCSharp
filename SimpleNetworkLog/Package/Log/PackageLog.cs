using SimpleNetwork.Client;
using SimpleNetwork.Package.Log.Entries;
using SimpleNetwork.Package.Log.Entries.Client;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    /// <summary>
    /// Stellt ein Paket Log für einen <see cref="SimpleNetwork.Client.IClient"/> da.
    /// </summary>
    [Serializable]
    public class PackageLog : IPackageLog
    {
        /// <summary>
        /// Der <see cref="SimpleNetwork.Client.IClient"/> für den das Log erstellt wurde.
        /// </summary>
        public IClient Client { get; protected set; }

        /// <summary>
        /// Eine Auflistung aller für diesen Client geloggten Pakete.
        /// </summary>
        public IEnumerable<IPackageLogEntry> AllPackages { get { return new List<IPackageLogEntry>(packages); } }

        /// <summary>
        /// Interne Sammlung aller geloggten Pakete.
        /// </summary>
        protected List<IPackageLogEntry> packages = new List<IPackageLogEntry>();

        /// <summary>
        /// Erstellt ein neues Log für den angegeben Client.
        /// </summary>
        /// <param name="client">Der Client für den das Log erstellt werden soll.</param>
        public PackageLog(IClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            Client = client;
            client.MessageIn += (s, e) =>
                {
                    addPackage(e.Package, PackageOrigin.Remote);
                };
            client.MessageOut += (s, e) =>
                {
                    addPackage(e.Package, PackageOrigin.Local);
                };
        }

        /// <summary>
        /// Gibt alle geloggten Pakete zurueck, die dem angegebenen Typ entsprechen.
        /// </summary>
        /// <typeparam name="T">Der Typ der zurueckgegebenen Pakete.</typeparam>
        /// <returns>Die geloggten Pakete des angegebenen Types.</returns>
        public IEnumerable<ITypedPackageLogEntry<T>> getPackages<T>() where T : IPackage
        {
            return packages.Where(x => x.Package.GetType() == typeof(T)).Select(x => (new TypedPackageLogEntry<T>((T)x.Package, x.Origin, x.TimeStamp)) as ITypedPackageLogEntry<T>);
        }

        /// <summary>
        /// Fuegt dem Log ein Paket hinzu.
        /// </summary>
        /// <typeparam name="T">Der Typ des hinzufuegten Paketes.</typeparam>
        /// <param name="package">Das hinzuzufuegende Paket.</param>
        /// <param name="origin">Die Herkunft des Paketes.</param>
        public void addPackage<T>(T package, PackageOrigin origin) where T : IPackage
        {
            if (package == null) throw new ArgumentNullException("package");
            if (package.shouldLogged())
                packages.Add(new PackageLogEntry(package, origin));
        }
    }
}

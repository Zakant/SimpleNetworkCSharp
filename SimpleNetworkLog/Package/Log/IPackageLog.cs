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
    /// Stellt ein Paket Log fuer einen <see cref="SimpleNetwork.Client.IClient"/> da.
    /// </summary>
    public interface IPackageLog
    {
        /// <summary>
        /// Der <see cref="SimpleNetwork.Client.IClient"/> fuer den das Log erstellt wurde.
        /// </summary>
        IClient Client { get; }

        /// <summary>
        /// Eine Auflistung aller fuer diesen Client geloggten Pakete.
        /// </summary>
        IEnumerable<IPackageLogEntry> AllPackages { get; }

        /// <summary>
        /// Gibt alle geloggten Pakete zurueck, die dem angegebenen Typ entsprechen.
        /// </summary>
        /// <typeparam name="T">Der Typ der zurueckgegebenen Pakete.</typeparam>
        /// <returns>Die geloggten Pakete des angegebenen Types.</returns>
        IEnumerable<ITypedPackageLogEntry<T>> getPackages<T>() where T : IPackage;

        /// <summary>
        /// Fuegt dem Log ein Paket hinzu.
        /// </summary>
        /// <typeparam name="T">Der Typ des hinzufuegten Paketes.</typeparam>
        /// <param name="package">Das hinzuzufuegende Paket.</param>
        /// <param name="origin">Die Herkunft des Paketes.</param>
        void addPackage<T>(T package, PackageOrigin origin) where T : IPackage;
    }
}

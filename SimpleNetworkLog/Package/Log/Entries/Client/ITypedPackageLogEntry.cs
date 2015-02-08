using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Client
{
    /// <summary>
    /// Stellt einen stark typisierten Eintrag in einem <see cref="SimpleNetwork.Package.Log.IPackageLog"/> da.
    /// </summary>
    /// <typeparam name="T">Der Paket Typ dieses Logs. Muss <see cref="SimpleNetwork.Package.Packages.IPackage"/> implementieren.</typeparam>
    public interface ITypedPackageLogEntry<T> where T : IPackage
    {
        /// <summary>
        /// Das geloggte Paket.
        /// </summary>
        T Package { get; }

        /// <summary>
        /// Die Herkunft des Paketes.
        /// </summary>
        PackageOrigin Origin { get; }

        /// <summary>
        /// Der Zeipunkt des Log Eintrages.
        /// </summary>
        DateTime TimeStamp { get; }
    }
}

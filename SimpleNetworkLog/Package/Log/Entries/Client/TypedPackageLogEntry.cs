using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Client
{
    /// <summary>
    /// Stellt einen stark typisierten Eintrag in einem <see cref="SimpleNetwork.Package.Log.PackageLog"/> da.
    /// </summary>
    /// <typeparam name="T">Der Paket Typ dieses Logs. Muss <see cref="SimpleNetwork.Package.Packages.IPackage"/> implementieren.</typeparam>
    [Serializable]
    public class TypedPackageLogEntry<T> : ITypedPackageLogEntry<T> where T : IPackage
    {
        /// <summary>
        /// Das geloggte Paket.
        /// </summary>
        public T Package { get; protected set; }

        /// <summary>
        /// Die Herkunft des Paketes.
        /// </summary>
        public PackageOrigin Origin { get; protected set; }

        /// <summary>
        /// Der Zeipunkt des Log Eintrages.
        /// </summary>
        public DateTime TimeStamp { get; protected set; }

        /// <summary>
        /// Erstellt einen neuen Log Eintrag.
        /// </summary>
        /// <param name="package">Das zu loggende Paket.</param>
        /// <param name="origin">Die Herkunft des Paketes.</param>
        public TypedPackageLogEntry(T package, PackageOrigin origin) : this(package, origin, DateTime.Now) { }

        /// <summary>
        /// Erstellt einen neuen Log Eintrag.
        /// </summary>
        /// <param name="package">Das zu loggende Paket.</param>
        /// <param name="origin">Die Herkunft des Paketes.</param>
        /// <param name="timeStamp">Der Zeitpunkt des Log Eintrages.</param>
        public TypedPackageLogEntry(T package, PackageOrigin origin, DateTime timeStamp)
        {
            Package = package;
            Origin = origin;
            TimeStamp = timeStamp;
        }
    }
}

using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Client
{
    /// <summary>
    /// Stellt einen Eintrag für alle <see cref="SimpleNetwork.Package.Packages.IPackage"/> in einem <see cref="SimpleNetwork.Package.Log.PackageLog"/> da.
    /// </summary>
    [Serializable]
    public class PackageLogEntry : IPackageLogEntry
    {
        /// <summary>
        /// Das geloggte Paket.
        /// </summary>
        public IPackage Package { get; protected set; }

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
        public PackageLogEntry(IPackage package, PackageOrigin origin) : this(package, origin, DateTime.Now) { }

        /// <summary>
        /// Erstellt einen neuen Log Eintrag.
        /// </summary>
        /// <param name="package">Das zu loggende Paket.</param>
        /// <param name="origin">Die Herkunft des Paketes.</param>
        /// <param name="timeStamp">Der Zeitpunkt des Log Eintrages.</param>
        public PackageLogEntry(IPackage package, PackageOrigin origin, DateTime timeStamp)
        {
            Package = package.Clone().hideFields();
            Origin = origin;
            TimeStamp = timeStamp;
        }
    }
}

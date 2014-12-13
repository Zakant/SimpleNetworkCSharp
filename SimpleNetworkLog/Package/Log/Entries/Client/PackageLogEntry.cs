using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Client
{
    [Serializable]
    public class PackageLogEntry : IPackageLogEntry
    {
        public IPackage Package { get; protected set; }

        public PackageOrigin Origin { get; protected set; }

        public DateTime TimeStamp { get; protected set; }

        public PackageLogEntry(IPackage package, PackageOrigin origin) : this(package, origin, DateTime.Now) { }

        public PackageLogEntry(IPackage package, PackageOrigin origin, DateTime timeStamp)
        {
            Package = package;
            Origin = origin;
            TimeStamp = timeStamp;
        }
    }
}

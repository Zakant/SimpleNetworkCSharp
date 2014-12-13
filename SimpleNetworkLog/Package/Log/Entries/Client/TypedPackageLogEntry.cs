using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Client
{
    [Serializable]
    public class TypedPackageLogEntry<T> : ITypedPackageLogEntry<T> where T : IPackage
    {
        public T Package { get; protected set; }

        public PackageOrigin Origin { get; protected set; }

        public DateTime TimeStamp { get; protected set; }

        public TypedPackageLogEntry(T package, PackageOrigin origin) : this(package, origin, DateTime.Now) { }

        public TypedPackageLogEntry(T package, PackageOrigin origin, DateTime timeStamp)
        {
            Contract.Requires<ArgumentNullException>(package != null);

            Package = package;
            Origin = origin;
            TimeStamp = timeStamp;
        }
    }
}

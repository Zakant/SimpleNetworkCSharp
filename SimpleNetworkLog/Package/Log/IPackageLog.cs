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
    public interface IPackageLog
    {
        IClient Client { get; }
        IEnumerable<IPackageLogEntry> AllPackages { get; }

        IEnumerable<ITypedPackageLogEntry<T>> getPackages<T>() where T : IPackage;

        void addPackage<T>(T package, PackageOrigin origin) where T : IPackage;
    }
}

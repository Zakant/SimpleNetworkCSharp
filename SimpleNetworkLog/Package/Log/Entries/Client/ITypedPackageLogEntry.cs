using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Client
{
    public interface ITypedPackageLogEntry<T> where T : IPackage
    {
        T Package { get; }
        PackageOrigin Origin { get; }
        DateTime TimeStamp { get; }
    }
}

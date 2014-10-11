using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.History
{
    public interface IPackageHistoryEntry<T> where T : IPackage
    {
        T Package { get; }
        PackageSource Source { get; }
    }
    public enum PackageSource : int
    {
        Remote,
        Local
    }
}

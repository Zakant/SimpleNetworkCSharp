using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Client
{
    /// <summary>
    /// Stellt einen Eintrag für alle <see cref="SimpleNetwork.Package.Packages.IPackage"/> in einem <see cref="SimpleNetwork.Package.Log.IPackageLog"/> da.
    /// </summary>
    public interface IPackageLogEntry : ITypedPackageLogEntry<IPackage>
    {
    }
}

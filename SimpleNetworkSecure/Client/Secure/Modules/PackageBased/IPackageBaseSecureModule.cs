using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client.Secure.Modules.PackageBased
{
    public interface IPackageBaseSecureModule : ISecureModule
    {

        IPackage encryptPackage(IPackage input);
        IPackage decryptPackage(IPackage input);
    }
}

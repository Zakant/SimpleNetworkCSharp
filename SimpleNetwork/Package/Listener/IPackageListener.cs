using SimpleNetwork.Client;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Listener
{
    public interface IPackageListener<T> where T : IPackage
    {
        void ProcessIncommingPackage(T package, IClient client);
    }
}

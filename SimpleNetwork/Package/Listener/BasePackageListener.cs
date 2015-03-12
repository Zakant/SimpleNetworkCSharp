using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Listener
{
    public abstract class BasePackageListener<T> : IPackageListener where T : IPackage
    {

        public abstract void ProcessIncommingPackage(T package, Client.IClient client);

        public void ProcessIncommingPackage(IPackage package, Client.IClient client)
        {
            ProcessIncommingPackage((T)package, client);
        }
    }
}

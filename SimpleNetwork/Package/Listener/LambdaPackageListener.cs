using SimpleNetwork.Client;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Listener
{
    public class LambdaPackageListener<T> : IPackageListener<T> where T : IPackage
    {

        private Action<T, IClient> _action;
        public void ProcessIncommingPackage(T package, Client.IClient client)
        {
            _action(package, client);
        }

        public LambdaPackageListener(Action<T, IClient> action)
        {
            _action = action;
        }
    }
}

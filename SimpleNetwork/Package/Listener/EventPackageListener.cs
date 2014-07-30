using SimpleNetwork.Client;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Listener
{
    public class EventPackageListener<T> : IPackageListener<T> where T : IPackage
    {

        public event EventHandler<EventPackageListenerEventArgs<T>> PackageReceived;

        public void ProcessIncommingPackage(T package, Client.IClient client)
        {
            var myevent = PackageReceived;
            if (myevent != null)
                myevent(this, new EventPackageListenerEventArgs<T>(package, client));
        }
    }
    public class EventPackageListenerEventArgs<T> : EventArgs where T : IPackage
    {
        public T Package { get; protected set; }
        public IClient Client { get; protected set; }

        public EventPackageListenerEventArgs(T package, IClient client)
        {
            Package = package;
            Client = client;
        }
    }
}

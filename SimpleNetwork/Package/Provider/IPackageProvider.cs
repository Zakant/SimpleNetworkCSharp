using SimpleNetwork.Events;
using SimpleNetwork.Package.Listener;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Provider
{
    public interface IPackageProvider
    {
        event EventHandler<NewMessageEventArgs> NewMessage;

        void RegisterPackageListener<T>(IPackageListener<T> packagelistener) where T : IPackage;

        void RemovePackageListener<T>(IPackageListener<T> packagelistener) where T : IPackage;
    }
}

using SimpleNetwork.Client;
using SimpleNetwork.Events;
using SimpleNetwork.Package.Listener;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Provider
{
    public abstract class PackageProvider : IPackageProvider
    {
        public event EventHandler<Events.NewMessageEventArgs> NewMessage;


        protected NewMessageEventArgs RaiseNewMessage(IPackage package, IClient c)
        {
            var myevent = NewMessage;
            var args = new NewMessageEventArgs(c, package);
            if (myevent != null)
                myevent(this, args);
            return args;
        }


        protected void informListener(IPackage package, IClient client)
        {
            if (_listener.ContainsKey(package.GetType()))
                foreach (var l in _listener[package.GetType()])
                {
                    l.GetType().GetMethod("ProcessIncommingPackage").Invoke(l, new object[] { package, client });
                }
        }

        private Dictionary<Type, IList<object>> _listener = new Dictionary<Type, IList<object>>();

        public void RegisterPackageListener<T>(IPackageListener<T> packagelistener) where T : IPackage
        {
            if (!_listener.ContainsKey(typeof(T)))
                _listener.Add(typeof(T), new List<object>());

            _listener[typeof(T)].Add(packagelistener);
        }

        public void RegisterPackageListener<T>(Action<T, IClient> action) where T : IPackage
        {
            RegisterPackageListener<T>(new LambdaPackageListener<T>(action));
        }

        public void RemovePackageListener<T>(IPackageListener<T> packagelistener) where T : IPackage
        {
            if (!_listener.ContainsKey(typeof(T))) return;

            _listener[typeof(T)].Remove(packagelistener);
            if (_listener[typeof(T)].Count == 0)
                _listener.Remove(typeof(T));
        }

        public void RemoveTypeListener<T>()
        {
            if (!_listener.ContainsKey(typeof(T))) return;
            _listener.Remove(typeof(T));
        }
    }
}

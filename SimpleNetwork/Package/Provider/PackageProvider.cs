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
    /// <summary>
    /// Stellt eine Abstrakte Basisklasse bereit, um Packete an <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> weiterzuleiten.
    /// </summary>
    public abstract class PackageProvider : IPackageProvider
    {

        /// <summary>
        /// Tritt ein, wenn eine neue Nachricht eintrifft.
        /// </summary>
        public event EventHandler<Events.NewMessageEventArgs> NewMessage;

        /// <summary>
        /// Löst das <see cref="PackageProvider.NewMessage" /> Ereignis aus.
        /// </summary>
        /// <param name="package">Das neu empfangene Packet.</param>
        /// <param name="c">Der Remotehost, der das Packet versendet hat.</param>
        /// <returns>Die verwendeten Erignis Argumente.</returns>
        protected NewMessageEventArgs RaiseNewMessage(IPackage package, IClient c)
        {
            var myevent = NewMessage;
            var args = new NewMessageEventArgs(c, package);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        /// <summary>
        /// Informiert alle <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />, die den entsprechenden Typ abboniert haben.
        /// </summary>
        /// <param name="package">Das Packet, das empfangen wurde.</param>
        /// <param name="client">Der RemoteHost, der das Packet versendet hat.</param>
        protected void informListener(IPackage package, IClient client)
        {
            if (_listener.ContainsKey(package.GetType()))
                foreach (var l in _listener[package.GetType()])
                {
                    l.GetType().GetMethod("ProcessIncommingPackage").Invoke(l, new object[] { package, client });
                }
        }

        private Dictionary<Type, IList<object>> _listener = new Dictionary<Type, IList<object>>();

        /// <summary>
        /// Registriert einen neuen <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.
        /// </summary>
        /// <typeparam name="T">Den Packettype, den der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> abbonieren möchte.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.</param>
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

        /// <summary>
        /// Entfernt einen <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> abboniert hat.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />, der entfernt werden soll.</param>
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

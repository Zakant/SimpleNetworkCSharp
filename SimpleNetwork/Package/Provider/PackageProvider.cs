using SimpleNetwork.Client;
using SimpleNetwork.Events;
using SimpleNetwork.Package.Listener;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Provider.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public event EventHandler<NewMessageEventArgs> NewMessage;

        /// <summary>
        /// Tritt ein, wenn eine Nachricht gesendet werden soll.
        /// </summary>
        public event EventHandler<MessageSendEventArgs> MessageSend;

        /// <summary>
        /// Löst das <see cref="PackageProvider.NewMessage" /> Ereignis aus.
        /// </summary>
        /// <param name="package">Das neu empfangene Packet.</param>
        /// <param name="client">Der Remotehost, der das Packet versendet hat.</param>
        /// <returns>Die verwendeten Ereignis Argumente.</returns>
        protected NewMessageEventArgs RaiseNewMessage(IPackage package, IClient client)
        {
            var myevent = NewMessage;
            var args = new NewMessageEventArgs(client, package);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        /// <summary>
        /// Löst das <see cref="PackageProvider.MessageSend" /> Ereignis aus.
        /// </summary>
        /// <param name="package">Das zu sendene Packet.</param>
        /// <param name="client">Der Host, der das Packet versenden möchte.</param>
        /// <param name="target">Der Remotehost, an den das Packet gesendet werden soll.</param>
        /// <returns>Die verwendeten Ereignis Argumente.</returns>
        protected MessageSendEventArgs RaiseSendMessage(IPackage package, IClient target)
        {
            var myevent = MessageSend;
            var args = new MessageSendEventArgs(target, package);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        /// <summary>
        /// Informiert alle <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />, die den entsprechenden Typ abboniert haben.
        /// </summary>
        /// <param name="package">Das Packet, das empfangen wurde.</param>
        /// <param name="client">Der Remotehost, der das Packet versendet hat.</param>
        protected void informListener(IPackage package, IClient client)
        {
            var li = listener.Where(x => x.AcceptType == package.GetType() || (x.AcceptSubType && package.GetType().IsAssignableFrom(x.AcceptType)));
            if (li.Any(x => x.ExclusivListener))
                foreach (var l in li.Where(x => x.ExclusivListener))
                    CallProcess(l, package, client);
            else
                foreach (var l in li)
                    CallProcess(l, package, client);
        }

        private void CallProcess(ListenerEntry entry, IPackage package, IClient client)
        {
            entry.Listener.GetType().GetMethod("ProcessIncommingPackage").Invoke(entry.Listener, new object[] { package, client });
        }

        private List<ListenerEntry> listener = new List<ListenerEntry>();

        /// <summary>
        /// Registriert einen neuen <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.
        /// </summary>
        /// <typeparam name="T">Den Packettype, den der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> abbonieren möchte.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.</param>
        public void RegisterPackageListener<T>(IPackageListener<T> packagelistener) where T : IPackage
        {
            listener.Add(new ListenerEntry(packagelistener, typeof(T)));
        }

        /// <summary>
        /// Registriert einen neuen <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.
        /// </summary>
        /// <typeparam name="T">Den Packettype, den der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> abbonieren möchte.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.</param>
        /// <param name="acceptSubTypes">Ein Wahrheitswert, der angibt, ob auch Sub Typen berücksichtigt werden soll</param>
        public void RegisterPackageListener<T>(IPackageListener<T> packagelistener, bool acceptSubTypes) where T : IPackage
        {
            listener.Add(new ListenerEntry(packagelistener, typeof(T), acceptSubTypes));
        }

        /// <summary>
        /// Reigstriert einen neunen Listener. Dieser wird durch eine <see cref="Action{T,T2}" /> dargestellt.
        /// </summary>
        /// <typeparam name="T">Der Typ, auf den gelauscht wird.</typeparam>
        /// <param name="action">Die <see cref="Action{T,T2}" />.</param>
        public void RegisterPackageListener<T>(Action<T, IClient> action) where T : IPackage
        {
            RegisterPackageListener<T>(new LambdaPackageListener<T>(action));
        }

        /// <summary>
        /// Reigstriert einen neunen Listener. Dieser wird durch eine <see cref="Action{T,T2}" /> dargestellt.
        /// </summary>
        /// <typeparam name="T">Der Typ, auf den gelauscht wird.</typeparam>
        /// <param name="action">Die <see cref="Action{T,T2}" />.</param>
        /// <param name="acceptSubTypes">Ein Wahrheitswert, der angibt, ob auch Sub Typen berücksichtigt werden soll</param>
        public void RegisterPackageListener<T>(Action<T, IClient> action, bool acceptSubTypes) where T : IPackage
        {
            RegisterPackageListener<T>(new LambdaPackageListener<T>(action), acceptSubTypes);
        }

        /// <summary>
        /// Entfernt einen <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" /> abboniert hat.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener{T}" />, der entfernt werden soll.</param>
        public void RemovePackageListener<T>(IPackageListener<T> packagelistener) where T : IPackage
        {
            listener.RemoveAll(x => x.Listener == packagelistener);
        }

        /// <summary>
        /// Entfernt alle Listener eines bestimmten Types.
        /// </summary>
        /// <typeparam name="T">Der Typ, von dem alle Listener entfernt werden sollen.</typeparam>
        public void RemoveTypeListener<T>() where T : IPackage
        {
            listener.RemoveAll(x => x.AcceptType == typeof(T));
        }

        protected void RegisterExclusivPackageListern<T>(IPackageListener<T> packagelistener) where T : IPackage
        {
            listener.Add(new ListenerEntry(packagelistener, typeof(T), false, true));
        }

        protected void RegisterExclusivPackageListern<T>(IPackageListener<T> packagelistener, bool acceptSubTypes) where T : IPackage
        {
            listener.Add(new ListenerEntry(packagelistener, typeof(T), acceptSubTypes, true));
        }

        protected void RegisterExclusivPackageListern<T>(Action<T, IClient> action) where T : IPackage
        {
            listener.Add(new ListenerEntry(new LambdaPackageListener<T>(action), typeof(T), false, true));
        }

        protected void RegisterExclusivPackageListern<T>(Action<T, IClient> action, bool acceptSubTypes) where T : IPackage
        {
            listener.Add(new ListenerEntry(new LambdaPackageListener<T>(action), typeof(T), acceptSubTypes, true));
        }
    }
}

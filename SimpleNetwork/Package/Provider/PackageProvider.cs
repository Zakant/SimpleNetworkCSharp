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
    /// Stellt eine Abstrakte Basisklasse bereit, um Packete an <see cref="SimpleNetwork.Package.Listener.IPackageListener" /> weiterzuleiten.
    /// </summary>
    public abstract class PackageProvider : IPackageProvider
    {

        /// <summary>
        /// Tritt ein, wenn eine neue Nachricht eintrifft.
        /// </summary>
        public event EventHandler<MessageInEventArgs> MessageIn;

        /// <summary>
        /// Tritt ein, wenn eine Nachricht gesendet werden soll.
        /// </summary>
        public event EventHandler<MessageOutEventArgs> MessageOut;

        /// <summary>
        /// Löst das <see cref="PackageProvider.MessageIn" /> Ereignis aus.
        /// </summary>
        /// <param name="package">Das neu empfangene Packet.</param>
        /// <param name="client">Der Remotehost, der das Packet versendet hat.</param>
        /// <returns>Die verwendeten Ereignis Argumente.</returns>
        protected MessageInEventArgs RaiseNewMessage(IPackage package, IClient client)
        {
            var myevent = MessageIn;
            var args = new MessageInEventArgs(client, package);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        /// <summary>
        /// Löst das <see cref="PackageProvider.MessageOut" /> Ereignis aus.
        /// </summary>
        /// <param name="package">Das zu sendene Packet.</param>
        /// <param name="target">Der Remotehost, an den das Packet gesendet werden soll.</param>
        /// <returns>Die verwendeten Ereignis Argumente.</returns>
        protected MessageOutEventArgs RaiseSendMessage(IPackage package, IClient target)
        {
            var myevent = MessageOut;
            var args = new MessageOutEventArgs(target, package);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        /// <summary>
        /// Informiert alle <see cref="SimpleNetwork.Package.Listener.IPackageListener" />, die den entsprechenden Typ abboniert haben.
        /// </summary>
        /// <param name="package">Das Packet, das empfangen wurde.</param>
        /// <param name="client">Der Remotehost, der das Packet versendet hat.</param>
        protected void informListener(IPackage package, IClient client)
        {
            var li = listener.Where(x => x.AcceptType == package.GetType() || (x.AcceptSubType && package.GetType().IsAssignableFrom(x.AcceptType))).ToList();
            if (li.Any(x => x.ExclusivListener))
                foreach (var l in li.Where(x => x.ExclusivListener))
                    CallProcess(l, package, client);
            else
                foreach (var l in li)
                    CallProcess(l, package, client);
        }

        private void CallProcess(ListenerEntry entry, IPackage package, IClient client)
        {
            entry.Listener.ProcessIncommingPackage(package, client);
        }

        private List<ListenerEntry> listener = new List<ListenerEntry>();

        /// <summary>
        /// Registriert einen neuen <see cref="SimpleNetwork.Package.Listener.IPackageListener" />.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den der Listener abbonieren soll.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener" />.</param>
        public void RegisterPackageListener<T>(IPackageListener packagelistener) where T : IPackage
        {
            listener.Add(new ListenerEntry(packagelistener, typeof(T)));
        }

        /// <summary>
        /// Registriert einen neuen <see cref="SimpleNetwork.Package.Listener.IPackageListener" />.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den der Listener abbonieren soll.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener" />.</param>
        /// <param name="acceptSubTypes">Ein Wahrheitswert, der angibt, ob auch Sub Typen berücksichtigt werden sollen.</param>
        public void RegisterPackageListener<T>(IPackageListener packagelistener, bool acceptSubTypes) where T : IPackage
        {
            listener.Add(new ListenerEntry(packagelistener, typeof(T), acceptSubTypes));
        }

        /// <summary>
        /// Reigstriert einen neunen Listener. Dieser wird durch eine <see cref="Action{T,T2}" /> dargestellt.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den der Listener abbonieren soll.</typeparam>
        /// <param name="action">Die <see cref="Action{T,T2}" />.</param>
        public void RegisterPackageListener<T>(Action<T, IClient> action) where T : IPackage
        {
            RegisterPackageListener<T>(new LambdaPackageListener<T>(action));
        }

        /// <summary>
        /// Reigstriert einen neunen Listener. Dieser wird durch eine <see cref="Action{T,T2}" /> dargestellt.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den der Listener abbonieren soll.</typeparam>
        /// <param name="action">Die <see cref="Action{T,T2}" />.</param>
        /// <param name="acceptSubTypes">Ein Wahrheitswert, der angibt, ob auch Sub Typen berücksichtigt werden sollen.</param>
        public void RegisterPackageListener<T>(Action<T, IClient> action, bool acceptSubTypes) where T : IPackage
        {
            RegisterPackageListener<T>(new LambdaPackageListener<T>(action), acceptSubTypes);
        }

        /// <summary>
        /// Entfernt einen <see cref="SimpleNetwork.Package.Listener.IPackageListener" />.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den <see cref="SimpleNetwork.Package.Listener.IPackageListener" /> abboniert hat.</typeparam>
        /// <param name="packagelistener">Der <see cref="SimpleNetwork.Package.Listener.IPackageListener" />, der entfernt werden soll.</param>
        public void RemovePackageListener<T>(IPackageListener packagelistener) where T : IPackage
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

        /// <summary>
        /// Registriet einen neunen Listener mit exclusiv Rechten über den angegeben Typen.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den der Listener abbonieren soll.</typeparam>
        /// <param name="packagelistener">Der Listener, der mit exclusiv Rechten registriert werden soll.</param>
        protected void RegisterExclusivPackageListern<T>(IPackageListener packagelistener) where T : IPackage
        {
            listener.Add(new ListenerEntry(packagelistener, typeof(T), false, true));
        }

        /// <summary>
        /// Registriet einen neunen Listener mit exclusiv Rechten über den angegeben Typen.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den der Listener abbonieren soll.</typeparam>
        /// <param name="packagelistener">Der Listener, der mit exclusiv Rechten registriert werden soll.</param>
        /// <param name="acceptSubTypes">Ein Wahrheitswert, der angibt, ob auch Sub Typen berücksichtigt werden sollen.</param>
        protected void RegisterExclusivPackageListern<T>(IPackageListener packagelistener, bool acceptSubTypes) where T : IPackage
        {
            listener.Add(new ListenerEntry(packagelistener, typeof(T), acceptSubTypes, true));
        }

        /// <summary>
        /// Registriet einen neunen Listener mit exclusiv Rechten über den angegeben Typen.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den der Listener abbonieren soll.</typeparam>
        /// <param name="action">Der Listener, der mit exclusiv Rechten registriert werden soll. Repräsentiert durch eine <see cref="Action{T, IClient}"/> Methode.</param>
        protected void RegisterExclusivPackageListern<T>(Action<T, IClient> action) where T : IPackage
        {
            listener.Add(new ListenerEntry(new LambdaPackageListener<T>(action), typeof(T), false, true));
        }

        /// <summary>
        /// Registriet einen neunen Listener mit exclusiv Rechten über den angegeben Typen.
        /// </summary>
        /// <typeparam name="T">Der Packettype, den der Listener abbonieren soll.</typeparam>
        /// <param name="action">Der Listener, der mit exclusiv Rechten registriert werden soll. Repräsentiert durch eine <see cref="Action{T, IClient}"/> Methode.</param>
        /// <param name="acceptSubTypes">Ein Wahrheitswert, der angibt, ob auch Sub Typen berücksichtigt werden sollen.</param>
        protected void RegisterExclusivPackageListern<T>(Action<T, IClient> action, bool acceptSubTypes) where T : IPackage
        {
            listener.Add(new ListenerEntry(new LambdaPackageListener<T>(action), typeof(T), acceptSubTypes, true));
        }
    }
}

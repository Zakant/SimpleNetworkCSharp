using SimpleNetwork.Client;
using SimpleNetwork.Package.Packages;
using System;

namespace SimpleNetwork.Package.Listener
{
    /// <summary>
    /// Stellte eine Klasse bereit, um auf eingehende Packete mithilfe von Ereignisen reagieren zu können.
    /// </summary>
    /// <typeparam name="T">Der Typ der Pakete, auf die reagiert werden soll.</typeparam>
    public class EventPackageListener<T> : BasePackageListener<T> where T : IPackage
    {
        /// <summary>
        /// Tritt ein, wenn ein neues Packet eingetroffen ist.
        /// </summary>
        public event EventHandler<EventPackageListenerEventArgs<T>> PackageReceived;

        /// <summary>
        /// Verarbeitet ein eingehendes Paket.
        /// </summary>
        /// <param name="package">Das eigehende Packet.</param>
        /// <param name="client">Der Remotehost, von dem das Packet stammt.</param>
        public override void ProcessIncommingPackage(T package, Client.IClient client)
        {
            var myevent = PackageReceived;
            if (myevent != null)
                myevent(this, new EventPackageListenerEventArgs<T>(package, client));
        }
    }
    /// <summary>
    /// Stellt Daten für das <see cref="EventPackageListener{T}.PackageReceived"/> Ereignis bereit.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventPackageListenerEventArgs<T> : EventArgs where T : IPackage
    {
        /// <summary>
        /// Das empfangene Packet.
        /// </summary>
        public T Package { get; protected set; }
        /// <summary>
        /// Der Remotehost, der das Packet gesendet hat.
        /// </summary>
        public IClient Client { get; protected set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der EventPackageListenerEventArgs unter verwendung des angegeben package und ICLient-Objekts.
        /// </summary>
        /// <param name="package">Das empfange und zubearbeitende Packet</param>
        /// <param name="client">Der Remotehost, der das Packet gesendet hat.</param>
        public EventPackageListenerEventArgs(T package, IClient client)
        {
            Package = package;
            Client = client;
        }
    }
}

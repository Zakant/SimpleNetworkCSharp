using SimpleNetwork.Client;
using SimpleNetwork.Package.Packages;
using System;

namespace SimpleNetwork.Package.Listener
{
    /// <summary>
    /// Stellte eine Klasse bereit, um auf eingehende Packete mithilfe von Lambda Methoden reagieren zu können.
    /// </summary>
    /// <typeparam name="T">Der Typ der Pakete, auf die reagiert werden soll.</typeparam>
    public class LambdaPackageListener<T> : BasePackageListener<T> where T : IPackage
    {

        private Action<T, IClient> _action;
        /// <summary>
        /// Verarbeitet ein eingehendes Paket.
        /// </summary>
        /// <param name="package">Das eigehende Packet.</param>
        /// <param name="client">Der Remotehost, von dem das Packet stammt.</param>
        public override void ProcessIncommingPackage(T package, Client.IClient client)
        {
            _action(package, client);
        }

        /// <summary>
        /// Initialisiert eine neue Instanz des LambdaPackageListener unter verwendung der angegeben Action.
        /// </summary>
        /// <param name="action">Die Action, die für jedes zu bearbeitende Packet aufgerufen wird.</param>
        public LambdaPackageListener(Action<T, IClient> action)
        {
            _action = action;
        }
    }
}

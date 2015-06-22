using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Listener
{
    /// <summary>
    /// Stellt eine abstrakte Basisklasse bereit, um eingehende Pakete zu verarbeiten.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BasePackageListener<T> : IPackageListener where T : IPackage
    {
        /// <summary>
        /// Verarbeitet ein eingehendes Paket.
        /// </summary>
        /// <param name="package">Das eingehende Paket.</param>
        /// <param name="client">Der Remotehost, von dem das Packet stammt.</param>
        public abstract void ProcessIncommingPackage(T package, Client.IClient client);

        /// <summary>
        /// Verarbeitet ein eingehendes, unbestimmtes, Paket.
        /// </summary>
        /// <param name="package">Das eingehende unbestimmte Paket.</param>
        /// <param name="client">Der Remotehost, von dem das Packet stammt.</param>
        public void ProcessIncommingPackage(IPackage package, Client.IClient client)
        {
            ProcessIncommingPackage((T)package, client);
        }
    }
}

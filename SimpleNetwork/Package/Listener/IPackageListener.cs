using SimpleNetwork.Client;
using SimpleNetwork.Package.Packages;

namespace SimpleNetwork.Package.Listener
{
    /// <summary>
    /// Stellt Methoden bereit um auf eigehende Packete reagieren zu können.
    /// </summary>
    /// <typeparam name="T">Der Typ der Pakete, auf die reagiert werden soll.</typeparam>
    public interface IPackageListener<T> where T : IPackage
    {
        /// <summary>
        /// Verarbeitet ein eingehendes Paket.
        /// </summary>
        /// <param name="package">Das eigehende Packet.</param>
        /// <param name="client">Der Remotehost, von dem das Packet stammt.</param>
        void ProcessIncommingPackage(T package, IClient client);
    }
}

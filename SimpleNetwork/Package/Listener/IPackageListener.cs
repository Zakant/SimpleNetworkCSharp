using SimpleNetwork.Client;
using SimpleNetwork.Package.Packages;

namespace SimpleNetwork.Package.Listener
{
    /// <summary>
    /// Stellt Methoden bereit um auf eigehende Packete reagieren zu können.
    /// </summary>
    public interface IPackageListener
    {
        /// <summary>
        /// Verarbeitet ein eingehendes Paket.
        /// </summary>
        /// <param name="package">Das eigehende Packet.</param>
        /// <param name="client">Der Remotehost, von dem das Packet stammt.</param>
        void ProcessIncommingPackage(IPackage package, IClient client);
    }
}

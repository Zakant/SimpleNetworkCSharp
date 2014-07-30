using SimpleNetwork.Client;
using SimpleNetwork.Events;
using SimpleNetwork.Package;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SimpleNetwork.Server
{
    /// <summary>
    /// Stellt Methoden bereit, um Verbindungen von Remotehosts entgegennehmen zu können.
    /// </summary>
    public interface IServer : IPackageProvider
    {
        /// <summary>
        /// Tritt ein, wenn einer neuer Remotehost verbindet.
        /// </summary>
        event EventHandler<ClientConnectedEventArgs> ClientConnected;

        /// <summary>
        /// Tritt ein, wenn die Verbindung zu einem Remotehost abbricht.
        /// </summary>
        event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        /// <summary>
        /// Stellt eine Auflistung aller verbundenen Remotehosts da.
        /// </summary>
        IEnumerable<IClient> Clients { get; }

        /// <summary>
        /// Stellt den Endpunkt da, an dem der Server auf eingehende Verbindungsanfragen lauscht.
        /// </summary>
        IPEndPoint EndPoint { get; }

        /// <summary>
        /// Stellt den Port da, auf dem der Server auf eigehnende Verbindungsanfragen lauscht.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Stellt einen Wert da, der angibt, ob der Server neue Verbindungsanfragen annimmt, oder ablehnt.
        /// </summary>
        bool AcceptNew { get; }

        /// <summary>
        /// Stellt die IClientFactory da, mitderen Hilfe der Server neue IClient-Objekte erzeugt.
        /// </summary>
        IClientFactory ClientFactory { get; set; }

        /// <summary>
        /// Starte den Sever.
        /// </summary>
        void Start();

        /// <summary>
        /// Stop den Server.
        /// </summary>
        void Stop();

        /// <summary>
        /// Sendet ein Packet an alle verbundenen Remotehosts.
        /// </summary>
        /// <param name="package"></param>
        void BroadcastPackage(IPackage package);

    }
}

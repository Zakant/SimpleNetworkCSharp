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
    public interface IServer : IPackageProvider
    {
        event EventHandler<ClientConnectedEventArgs> ClientConnected;

        event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        IEnumerable<IClient> Clients { get; }

        IPEndPoint EndPoint { get; }

        int Port { get; }

        bool AcceptNew { get; }

        IClientFactory ClientFactory { get; set; }

        void Start();

        void Stop();

        void BroadcastPackage(IPackage package);

    }
}

using SimpleNetwork.Detection;
using SimpleNetwork.Detection.Data;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SimpleNetwork.Client
{
    public interface IClient : IPackageProvider
    {
        event EventHandler<SimpleNetwork.Events.DisconnectedEventArgs> Disconnected;

        bool isConnected { get; }

        void Connect(IPAddress ip, int port);

        void Connect(HostData data);

        void Disconnect();

        void StartListening();

        void StopListening();

        void SendPackage(IPackage package);

    }
}

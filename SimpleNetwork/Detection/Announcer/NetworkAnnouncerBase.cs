using SimpleNetwork.Detection.Data;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleNetwork.Detection.Announcer
{
    public abstract class NetworkAnnouncerBase : INetworkAnnouncer
    {
        public IServer Server { get; protected set; }

        public int Intervall { get; protected set; }

        public int AnnouncingPort { get; set; }

        public bool Announcing { get; protected set; }

        public HostData Data { get; set; }

        public NetworkAnnouncerBase(IServer server)
        {
            Server = server;
            Announcing = false;
            Intervall = 3000;
            AnnouncingPort = 15000;
            Data = new HostData();
            Data.ID = Guid.NewGuid();
            Data.Addresses = GetIPAddress();
            Data.Port = server.EndPoint.Port;
        }


        protected static IPAddress[] GetIPAddress()
        {
            return Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToArray();
        }


        public abstract void StartAnnouncing();

        public abstract void StopAnnouncing();

    }
}

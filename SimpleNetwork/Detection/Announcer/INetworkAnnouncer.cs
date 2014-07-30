using SimpleNetwork.Detection.Data;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Detection.Announcer
{
    public interface INetworkAnnouncer
    {
        IServer Server { get; }

        int Intervall { get; }

        int AnnouncingPort { get; set; }

        bool Announcing { get; }

        HostData Data { get; set; }

        void StartAnnouncing();

        void StopAnnouncing();

    }
}

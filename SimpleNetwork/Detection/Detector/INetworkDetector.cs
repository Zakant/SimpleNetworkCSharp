using SimpleNetwork.Detection.Data;
using SimpleNetwork.Detection.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Detection.Detector
{
    public interface INetworkDetector
    {
        event EventHandler<HostFoundEventArgs> HostFound;

        event EventHandler<HostLostEventArgs> HostLost;

        IEnumerable<HostData> Hosts { get; }

        bool isDetecting { get; }

        int DetectionPort { get; set; }

        TimeSpan LiveTime { get; set; }

        void StartDetection();

        void Refresh();

        void StopDetection();
    }
}

using SimpleNetwork.Detection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace SimpleNetwork.Detection.Detector
{
    public abstract class NetworkDetectorBase : INetworkDetector
    {
        public event EventHandler<Events.HostFoundEventArgs> HostFound;
        protected Events.HostFoundEventArgs RaiseHostFound(HostData newData)
        {
            var myevent = HostFound;
            var args = new Events.HostFoundEventArgs(newData);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        public event EventHandler<Events.HostLostEventArgs> HostLost;
        protected Events.HostLostEventArgs RaiseHostLost(HostData oldData)
        {
            var myevent = HostLost;
            var args = new Events.HostLostEventArgs(oldData);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        public int DetectionPort { get; set; }

        public TimeSpan LiveTime { get; set; }

        public bool isDetecting { get; protected set; }

        protected List<HostDataTime> _hosts = new List<HostDataTime>();
        public virtual IEnumerable<HostData> Hosts
        {
            get { return new List<HostData>(_hosts.Select(x => x.data)); }
        }

        public NetworkDetectorBase()
        {
            DetectionPort = 15000;
            LiveTime = new TimeSpan(0, 0, 5);
        }


        private Timer _removetimer;

        protected void StartCleanUp()
        {
            _removetimer = new Timer();
            _removetimer.AutoReset = true;
            _removetimer.Elapsed += new ElapsedEventHandler((sender, args) =>
            {
                List<HostDataTime> temp = new List<HostDataTime>();
                foreach (var h in _hosts)
                {
                    if ((DateTime.Now - h.stamp) <= LiveTime)
                        temp.Add(h);
                    else
                        RaiseHostLost(h.data);
                }
                _hosts = temp;
            });
            _removetimer.Interval = LiveTime.TotalMilliseconds / 2;
            _removetimer.Start();
        }

        protected void StopCleanUp()
        {
            _removetimer.Stop();
            _removetimer.Dispose();
        }

        protected void Clear()
        {
            foreach (var d in _hosts)
            {
                RaiseHostLost(d.data);
            }
            _hosts.Clear();
        }

        protected static IPAddress[] GetIPAddress()
        {
            return Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToArray();
        }

        public abstract void StartDetection();

        public abstract void Refresh();

        public abstract void StopDetection();
    }
}

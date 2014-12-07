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
    /// <summary>
    /// Basisklasse, die Methoden bereitstellt,die es ermöglichen, Server im lokalen Netzwerk zu entdecken.
    /// </summary>
    public abstract class NetworkDetectorBase : INetworkDetector
    {

        /// <summary>
        /// Lock Object das verwendet wird um beim CleanUp Race-Conditions zu verhindern.
        /// </summary>
        protected object LockObject = new object();

        /// <summary>
        /// Tritt ein, wenn ein neuer Host gefunden wurde.
        /// </summary>
        public event EventHandler<Events.HostFoundEventArgs> HostFound;

        /// <summary>
        /// Löst das <see cref="HostFound"/> Ereignis aus.
        /// </summary>
        /// <param name="newData">Die Serverinformationen des neu gefunden Hosts.</param>
        /// <returns>Die verwendeten Ereignis Argumente.</returns>
        protected Events.HostFoundEventArgs RaiseHostFound(HostData newData)
        {
            var myevent = HostFound;
            var args = new Events.HostFoundEventArgs(newData);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        /// <summary>
        /// Tritt ein, wenn ein Host verloren wurde.
        /// </summary>
        public event EventHandler<Events.HostLostEventArgs> HostLost;
        /// <summary>
        /// Löst das <see cref="HostLost"/> Ereigniss aus.
        /// </summary>
        /// <param name="oldData">Die Serverinformationen des Hosts, der verloren wurde.</param>
        /// <returns>Die verwendeten Ereignis Argumente.</returns>
        protected Events.HostLostEventArgs RaiseHostLost(HostData oldData)
        {
            var myevent = HostLost;
            var args = new Events.HostLostEventArgs(oldData);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        /// <summary>
        /// Der Port auf dem nach Hosts gesucht wird.
        /// </summary>
        public int DetectionPort { get; set; }

        /// <summary>
        /// Die Zeitspanne, die ein gefundener Host als gülltig betrachtet wird, ohne erneut gefunden worden zu sein.
        /// </summary>
        public TimeSpan LiveTime { get; set; }

        /// <summary>
        /// Zeigt an, ob aktuell die suche nach Hosts läuft. True, falls ja, False, falls nein.
        /// </summary>
        public bool isDetecting { get; protected set; }

        /// <summary>
        /// Die intern geführte Liste der gefundenen Serverinformationen mit dem zusätzlichen Timestampihres fundes.
        /// </summary>
        protected List<HostDataTime> _hosts = new List<HostDataTime>();
        /// <summary>
        /// Eine Auflistung, die alle verfügbaren Hosts beinhaltet.
        /// </summary>
        public virtual IEnumerable<HostData> Hosts
        {
            get { return new List<HostData>(_hosts.Select(x => x.data)); }
        }

        /// <summary>
        /// Initialisert eine neue Instanz der NetworkDetectorBase-Klasse.
        /// </summary>
        public NetworkDetectorBase()
        {
            DetectionPort = 15000;
            LiveTime = new TimeSpan(0, 0, 5);
        }


        private Timer _removetimer;

        /// <summary>
        /// Sorgt dafür, dass alle Serverinformationen, die älter als <see cref="LiveTime"/> sind periodisch gelöscht werden.
        /// </summary>
        protected void StartCleanUp()
        {
            _removetimer = new Timer();
            _removetimer.AutoReset = true;
            _removetimer.Elapsed += new ElapsedEventHandler((sender, args) =>
            {
                lock (LockObject)
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
                }
            });
            _removetimer.Interval = LiveTime.TotalMilliseconds / 2;
            _removetimer.Start();
        }

        /// <summary>
        /// Beendet das periodische löschen von Serverinformationen, die älter als <see cref="LiveTime"/> sind.
        /// </summary>
        protected void StopCleanUp()
        {
            _removetimer.Stop();
            _removetimer.Dispose();
        }

        /// <summary>
        /// Löscht alle vorhandenen Serverinformationen und löst für jede das <see cref="HostLost"/> Ereignis aus.
        /// </summary>
        protected void Clear()
        {
            foreach (var d in _hosts)
            {
                RaiseHostLost(d.data);
            }
            _hosts.Clear();
        }

        /// <summary>
        /// Liefert eine Auflistung aller IPv4 Adressen dieses Computers zurück.
        /// </summary>
        /// <returns>Das Array der verfügbaren IP Adressen</returns>
        protected static IPAddress[] GetIPAddress()
        {
            return Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToArray();
        }

        /// <summary>
        /// Beginnt mit der Erfassung von Servern.
        /// </summary>
        public abstract void StartDetection();

        /// <summary>
        /// Aktualisert die vorhandenen Serverinformationen.
        /// </summary>
        public abstract void Refresh();

        /// <summary>
        /// Beendet die Erfassung von Servern.
        /// </summary>
        public abstract void StopDetection();

        /// <summary>
        /// Gibt diesen Netzwerk Detektor und alle verwendeten Resourcen frei.
        /// </summary>
        public virtual void Dispose()
        {
            StopDetection();
        }
    }
}

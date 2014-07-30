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
    /// <summary>
    /// Basisklasse, die ermöglicht, Server im lokalen Netzwerk aufzuspüren
    /// </summary>
    public abstract class NetworkAnnouncerBase : INetworkAnnouncer
    {
        /// <summary>
        /// Der Server, dessen Verbindungsinformationen mitgeteilt werden sollen. ReadOnly.
        /// </summary>
        public IServer Server { get; protected set; }

        /// <summary>
        /// Der Interval, in dem die Serverinformationen versendet werden soll. ReadOnly.
        /// </summary>
        public int Interval { get; protected set; }

        /// <summary>
        /// Der Port, auf dem  die Serverinformationen versendet werden sollen.
        /// </summary>
        public int AnnouncingPort { get; set; }

        /// <summary>
        /// Gibt an, ob aktuell Serverinformationen versendet werden. True falls ja, False falls nein. ReadOnly.
        /// </summary>
        public bool Announcing { get; protected set; }

        /// <summary>
        /// Die Daten aus der die Serverinformationen besteht.
        /// </summary>
        public HostData Data { get; set; }

        /// <summary>
        /// Initialisiert eine neue NetworkAnnouncerBase-Klasse unter verwendung eines IServer-Objektes.
        /// </summary>
        /// <param name="server">Das zu verwendene ISever-Objekt.</param>
        public NetworkAnnouncerBase(IServer server)
        {
            Server = server;
            Announcing = false;
            Interval = 3000;
            AnnouncingPort = 15000;
            Data = new HostData();
            Data.ID = Guid.NewGuid();
            Data.Addresses = GetIPAddress();
            Data.Port = server.EndPoint.Port;
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
        /// Beginnt die Serverinformationen zu versenden.
        /// </summary>
        public abstract void StartAnnouncing();

        /// <summary>
        /// Stopt die Serverinformationen zu versenden.
        /// </summary>
        public abstract void StopAnnouncing();

    }
}

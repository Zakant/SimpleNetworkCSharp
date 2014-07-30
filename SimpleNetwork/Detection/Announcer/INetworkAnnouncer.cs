using SimpleNetwork.Detection.Data;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Detection.Announcer
{
    /// <summary>
    /// Stellt Methoden bereit, um einen Server inheralb eines Lokalen Netzwerkes aufspüren zu können.
    /// </summary>
    public interface INetworkAnnouncer
    {
        /// <summary>
        /// Der Server, dessen Verbindungsinformationen mitgeteilt werden sollen. ReadOnly.
        /// </summary>
        IServer Server { get; }

        /// <summary>
        /// Der Interval, in dem die Mitteilung wiederholt werden soll. ReadOnly.
        /// </summary>
        int Interval { get; }

        /// <summary>
        /// Der Port, auf dem anderen die Mitteilung versendet werden soll.
        /// </summary>
        int AnnouncingPort { get; set; }

        /// <summary>
        /// Gibt an, ob aktuell Mitteilungen versendet werden. True falls ja, False falls nein. ReadOnly.
        /// </summary>
        bool Announcing { get; }

        /// <summary>
        /// Die Daten aus der die Mitteilung besteht.
        /// </summary>
        HostData Data { get; set; }

        /// <summary>
        /// Beginnt Mitteilungen zu versenden.
        /// </summary>
        void StartAnnouncing();

        /// <summary>
        /// Stopt Mitteilungen zu versenden.
        /// </summary>
        void StopAnnouncing();

    }
}

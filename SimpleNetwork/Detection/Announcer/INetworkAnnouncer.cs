using SimpleNetwork.Detection.Data;
using SimpleNetwork.Server;
using System;

namespace SimpleNetwork.Detection.Announcer
{
    /// <summary>
    /// Stellt Methoden bereit, um einen Server inheralb eines Lokalen Netzwerkes aufspüren zu können.
    /// </summary>
    public interface INetworkAnnouncer : IDisposable
    {
        /// <summary>
        /// Der Server, dessen Verbindungsinformationen mitgeteilt werden sollen. ReadOnly.
        /// </summary>
        IServer Server { get; }

        /// <summary>
        /// Der Interval, in dem die Serverinformationen versendet werden soll. ReadOnly.
        /// </summary>
        int Interval { get; }

        /// <summary>
        /// Der Port, auf dem  die Serverinformationen versendet werden sollen.
        /// </summary>
        int AnnouncingPort { get; set; }

        /// <summary>
        /// Gibt an, ob aktuell Serverinformationen versendet werden. True falls ja, False falls nein. ReadOnly.
        /// </summary>
        bool Announcing { get; }

        /// <summary>
        /// Die Daten aus der die Serverinformationen besteht.
        /// </summary>
        HostData Data { get; set; }

        /// <summary>
        /// Beginnt die Serverinformationen zu versenden.
        /// </summary>
        void StartAnnouncing();

        /// <summary>
        /// Stopt die Serverinformationen zu versenden.
        /// </summary>
        void StopAnnouncing();

    }
}

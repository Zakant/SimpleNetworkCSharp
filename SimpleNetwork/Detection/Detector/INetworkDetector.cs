using SimpleNetwork.Detection.Data;
using SimpleNetwork.Detection.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Detection.Detector
{
    /// <summary>
    /// Stellt Mehtoden bereit, um Server im lokalen Netzwerk zu finden.
    /// </summary>
    public interface INetworkDetector
    {
        /// <summary>
        /// Tritt ein, wenn ein neuer Host gefunden wurde.
        /// </summary>
        event EventHandler<HostFoundEventArgs> HostFound;

        /// <summary>
        /// Tritt ein, wenn ein Host verloren wurde.
        /// </summary>
        event EventHandler<HostLostEventArgs> HostLost;

        /// <summary>
        /// Eine Auflistung, die alle verfügbaren Hosts beinhaltet.
        /// </summary>
        IEnumerable<HostData> Hosts { get; }

        /// <summary>
        /// Zeigt an, ob aktuell die suche nach Hosts läuft. True, falls ja, False, falls nein.
        /// </summary>
        bool isDetecting { get; }

        /// <summary>
        /// Der Port auf dem nach Hosts gesucht wird.
        /// </summary>
        int DetectionPort { get; set; }

        /// <summary>
        /// Die Zeitspanne, die ein gefundener Host als gülltig betrachtet wird, ohne erneut gefunden worden zu sein.
        /// </summary>
        TimeSpan LiveTime { get; set; }

        /// <summary>
        /// Beginnt mit der Erfassung von Servern.
        /// </summary>
        void StartDetection();

        /// <summary>
        /// Aktualisert die vorhandenen Serverinformationen.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Beendet die Erfassung von Servern.
        /// </summary>
        void StopDetection();
    }
}

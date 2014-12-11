using SimpleNetwork.Detection.Data;
using System;

namespace SimpleNetwork.Detection.Events
{
    /// <summary>
    /// Stellt Daten für das <see cref="SimpleNetwork.Detection.Detector.INetworkDetector.HostLost"/> Ereigniss bereit.
    /// </summary>
    [Serializable]
    public class HostLostEventArgs : EventArgs
    {
        /// <summary>
        /// Die Daten des verlorenen Hosts.
        /// </summary>
        public HostData OldData { get; protected set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der HostFoundEventArgs unter verwendung des angegeben HostData-Objekts.
        /// </summary>
        /// <param name="oldData">Die Daten des verlorenen Hosts.</param>
        public HostLostEventArgs(HostData oldData)
        {
            this.OldData = oldData;
        }
    }
}

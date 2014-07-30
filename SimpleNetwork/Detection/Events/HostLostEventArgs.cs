using SimpleNetwork.Detection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// <param name="newData">Die Daten des verlorenen Hosts.</param>
        public HostLostEventArgs(HostData oldData)
        {
            this.OldData = oldData;
        }
    }
}

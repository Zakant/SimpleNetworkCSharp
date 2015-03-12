using SimpleNetwork.Detection.Data;
using System;

namespace SimpleNetwork.Detection.Events
{
    /// <summary>
    /// Stellt Daten für das <see cref="SimpleNetwork.Detection.Detector.INetworkDetector.HostFound"/> Ereignis bereit.
    /// </summary>
    [Serializable]
    public class HostFoundEventArgs : EventArgs
    {
        /// <summary>
        /// Die Daten des neu gefunden Hosts.
        /// </summary>
        public HostData NewData { get; protected set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der HostFoundEventArgs unter verwendung des angegeben HostData-Objekts.
        /// </summary>
        /// <param name="newData">Die Daten des neugefundenen Hosts.</param>
        public HostFoundEventArgs(HostData newData)
        {
            NewData = newData;
        }

    }
}

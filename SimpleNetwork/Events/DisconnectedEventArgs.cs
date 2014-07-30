using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Events
{
    /// <summary>
    /// Stellt Daten für das <see cref="SimpleNetwork.Client.IClient.Disconnected"/> Ereigniss bereit.
    /// </summary>
    public class DisconnectedEventArgs : SimpleNetworkEventArgs
    {
        /// <summary>
        /// Der Grund für den Verbindungsabbruch.
        /// </summary>
        public DisconnectReason Reason { get; protected set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der HostFoundEventArgs unter verwendung des angegeben Grundes .
        /// </summary>
        /// <param name="r">Der Grund.</param>
        public DisconnectedEventArgs(DisconnectReason r)
        {
            Reason = r;
        }

    }
}

using SimpleNetwork.Client;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Events
{
    public class MessageOutEventArgs : SimpleNetworkEventArgs
    {
        /// <summary>
        /// Der Remotehost, an den das Paket geht.
        /// </summary>
        public IClient Target { get; protected set; }
        /// <summary>
        /// Das empfangene Packet.
        /// </summary>
        public IPackage Package { get; protected set; }
        /// <summary>
        /// Der Typ des empfangenen Packetes.
        /// </summary>
        public Type MessageType { get { return (this.Package.GetType()); } }

        /// <summary>
        /// Zeigt an, ob die Nachricht bereits behandelt wurde.
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der NewMessageEventArgs unter verwendung des angegeben IClient-Objekts und dem IPackage-Objekt.
        /// </summary>
        /// <param name="client">Der IClient, von dem die Nachricht stammt.</param>
        /// <param name="package">Das empfangene Packet.</param>
        public MessageOutEventArgs(IClient target, IPackage package)
        {
            Target = target;
            Package = package;
            Handled = false;
        }

    }
}

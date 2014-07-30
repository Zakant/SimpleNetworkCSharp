using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Events
{
    public class DisconnectedEventArgs : SimpleNetworkEventArgs
    {
        public DisconnectReason Reason { get; protected set; }

        public DisconnectedEventArgs(DisconnectReason r)
        {
            Reason = r;
        }

    }
}

using SimpleNetwork.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Internal
{
    [Serializable]
    internal class ShutDownPackage : IPackage
    {
        public string ShutDownMessage { get; set; }
        public DisconnectReason Reason { get; set; }

        public ShutDownPackage(string Message)
        {
            ShutDownMessage = Message;
            Reason = DisconnectReason.ClosedProperly;
        }

        public ShutDownPackage(string Message,DisconnectReason reason)
        {
            ShutDownMessage = Message;
            Reason = reason;
        }
    }
}

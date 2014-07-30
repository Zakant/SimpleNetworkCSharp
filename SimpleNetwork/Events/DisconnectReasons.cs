using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Events
{
    public enum DisconnectReason : byte
    {
        Failed,
        Unknown,
        ConnectionShoutDown,
        ClosedProperly,
        LostConnection

    }
}

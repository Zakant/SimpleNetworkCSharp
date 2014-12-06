using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client.Secure
{
    public enum ConnectionState : int
    {
        Undefined,
        Unsecure,
        Securing,
        Secure
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

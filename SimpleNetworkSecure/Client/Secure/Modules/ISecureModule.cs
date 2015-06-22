using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client.Secure.Modules
{
    public interface ISecureModule
    {
        bool isReady { get; }
    }
}

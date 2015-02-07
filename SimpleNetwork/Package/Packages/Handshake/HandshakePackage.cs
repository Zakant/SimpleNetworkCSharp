using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Handshake
{
    public abstract class HandshakePackage : IPackage
    {
        public Dictionary<string, string> KeyValues { get; protected set; }

        public HandshakePackage()
        {
            KeyValues = new Dictionary<string, string>();
        }
    }
}

using SimpleNetwork.Client.Secure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Server.Secure
{
    public class SecureServer : Server, ISecureServer
    {
        public SecureServer(int port)
            : base(port)
        {
            ClientFactory = new SecureClientFactory();
        }

    }
}

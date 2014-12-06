﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client.Secure
{
    public class SecureClientFactory : IClientFactory
    {
        public IClient createFromTcpClient(System.Net.Sockets.TcpClient client)
        {
            return new SecureClient(client);
        }

        public IClient createDefault()
        {
            return new SecureClient();
        }
    }
}

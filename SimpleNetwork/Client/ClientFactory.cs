using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client
{
    public class ClientFactory : IClientFactory
    {
        public IClient createFromTcpClient(System.Net.Sockets.TcpClient client)
        {
            return new Client(client);
        }

        public IClient createDefault()
        {
            return new Client();
        }
    }
}

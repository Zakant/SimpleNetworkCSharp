using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SimpleNetwork.Client
{
    public interface IClientFactory
    {
        IClient createFromTcpClient(TcpClient client);
        IClient createDefault();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client
{
    /// <summary>
    /// Eine Fabrik zum erstellen von IClient-Objekten
    /// </summary>
    public class ClientFactory : IClientFactory
    {
        /// <summary>
        /// Erzeugt ein neues Client-Objekt aus einem angegebenen TcpClient
        /// </summary>
        /// <param name="client">Das bei der Erstellung zu verwendene TcpClient-Objekt</param>
        /// <returns>Das neu erstellte Client-Objekt</returns>
        public IClient createFromTcpClient(System.Net.Sockets.TcpClient client)
        {
            return new Client(client);
        }

        /// <summary>
        /// Erzeugt ein neues leeres Client-Objekt
        /// </summary>
        /// <returns>Das neue leere Client-Objekt</returns>
        public IClient createDefault()
        {
            return new Client();
        }
    }
}

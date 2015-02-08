
namespace SimpleNetwork.Client
{
    /// <summary>
    /// Eine Fabrik Klasse zum erzeugen von <see cref="SimpleNetwork.Client.Client"/> Objekten.
    /// </summary>
    public class ClientFactory : IClientFactory
    {
        /// <summary>
        /// Erzeugt ein neues Client-Objekt aus einem angegebenen TcpClient
        /// </summary>
        /// <param name="client">Das bei der Erstellung zu verwendene TcpClient-Objekt</param>
        /// <returns>Das neu erstellte IClient-Objekt</returns>
        public IClient createFromTcpClient(System.Net.Sockets.TcpClient client)
        {
            return new Client(client);
        }

        /// <summary>
        /// Erzeugt ein neues leeres IClient-Objekt
        /// </summary>
        /// <returns>Das neue leere IClient-Objekt</returns>
        public IClient createDefault()
        {
            return new Client();
        }
    }
}

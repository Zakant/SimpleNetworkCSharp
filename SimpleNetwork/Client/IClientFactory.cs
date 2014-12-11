using System.Net.Sockets;

namespace SimpleNetwork.Client
{
    /// <summary>
    /// Stellt Methoden bereit, um IClient-Objekte zu erstellen
    /// </summary>
    public interface IClientFactory
    {
        /// <summary>
        /// Erzeugt ein neues IClient-Objekt aus einem angegebenen TcpClient
        /// </summary>
        /// <param name="client">Das bei der Erstellung zu verwendene TcpClient-Objekt</param>
        /// <returns>Das neu erstellte IClient-Objekt</returns>
        IClient createFromTcpClient(TcpClient client);
        
        /// <summary>
        /// Erzeugt ein neues leeres IClient-Objekt
        /// </summary>
        /// <returns>Das neue leere IClient-Objekt</returns>
        IClient createDefault();
    }
}

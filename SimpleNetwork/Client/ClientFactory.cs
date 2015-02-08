
namespace SimpleNetwork.Client
{
    /// <summary>
    /// Eine Fabrik Klasse zum erzeugen von <see cref="SimpleNetwork.Client.Client"/> Objekten.
    /// </summary>
    public class ClientFactory : IClientFactory
    {
        /// <summary>
        /// Erstellt einen <see cref="SimpleNetwork.Client.Client"/> aus einem <see cref="System.Net.Sockets.TcpClient"/>.
        /// </summary>
        /// <param name="client">Der <see cref="System.Net.Sockets.TcpClient"/> aus dem der <see cref="SimpleNetwork.Client.Client"/> erstellt werden soll.</param>
        /// <returns>Der neu erstellte <see cref="SimpleNetwork.Client.Client"/>.</returns>
        public IClient createFromTcpClient(System.Net.Sockets.TcpClient client)
        {
            return new Client(client);
        }

        /// <summary>
        /// Erstekkt ein neues <see cref="SimpleNetwork.Client.Client"/> Objekt.
        /// </summary>
        /// <returns>Der neu erstellte <see cref="SimpleNetwork.Client.Client"/>.</returns>
        public IClient createDefault()
        {
            return new Client();
        }
    }
}

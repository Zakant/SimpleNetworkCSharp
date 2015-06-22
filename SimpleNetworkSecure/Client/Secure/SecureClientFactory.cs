
namespace SimpleNetwork.Client.Secure
{
    /// <summary>
    /// Eine Fabrik Klasse zum erzeugen von <see cref="SimpleNetwork.Client.Secure.SecureClient"/> Objekten.
    /// </summary>
    public class DhSecureClientFactory : IClientFactory
    {
        /// <summary>
        /// Erstellt einen <see cref="SimpleNetwork.Client.Secure.SecureClient"/> aus einem <see cref="System.Net.Sockets.TcpClient"/>.
        /// </summary>
        /// <param name="client">Der <see cref="System.Net.Sockets.TcpClient"/> aus dem der <see cref="SimpleNetwork.Client.Secure.SecureClient"/> erstellt werden soll.</param>
        /// <returns>Der neu erstellte <see cref="SimpleNetwork.Client.Secure.SecureClient"/>.</returns>
        public IClient createFromTcpClient(System.Net.Sockets.TcpClient client)
        {
            return new SecureClient(client);
        }

        /// <summary>
        /// Erstekkt ein neues <see cref="SimpleNetwork.Client.Secure.SecureClient"/> Objekt.
        /// </summary>
        /// <returns>Der neu erstellte <see cref="SimpleNetwork.Client.Secure.SecureClient"/>.</returns>
        public IClient createDefault()
        {
            return new SecureClient();
        }
    }
}

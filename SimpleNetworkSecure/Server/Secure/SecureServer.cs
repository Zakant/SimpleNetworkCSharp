using SimpleNetwork.Client.Secure;

namespace SimpleNetwork.Server.Secure
{
    /// <summary>
    /// Stellt eine Klasse zum entgennehmen von sicheren Verbindungen da.
    /// </summary>
    public class SecureServer : Server, ISecureServer
    {
        /// <summary>
        /// Erstellt einen neue Klass zum entgennehmen von sicheren Verbindungen.
        /// </summary>
        /// <param name="port">Der zu verwendene Port.</param>
        public SecureServer(int port)
            : base(port)
        {
            ClientFactory = new DhSecureClientFactory();
        }

    }
}

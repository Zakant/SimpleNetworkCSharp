using SimpleNetwork.Client.Secure;

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

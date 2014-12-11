using System;

namespace SimpleNetwork.Package.Packages.Internal.Secure
{
    [Serializable]
    public class ClientSecurePackage : IPackage
    {
        public byte[] PublicKey { get; protected set; }

        public ClientSecurePackage(byte[] publickey)
        {
            PublicKey = publickey;
        }
    }
}

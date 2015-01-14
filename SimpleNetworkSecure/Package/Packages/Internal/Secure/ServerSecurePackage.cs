using Mono.Security.Cryptography;
using System;


namespace SimpleNetwork.Package.Packages.Internal.Secure
{
    [Serializable]
    public class ServerSecurePackage : IPackage
    {
        public DHParameters Paramters { get; protected set; }

        public byte[] PublicKey { get; protected set; }

        public byte[] IV { get; protected set; }

        public ServerSecurePackage(byte[] publickey, byte[] iv, DHParameters parameters)
        {
            PublicKey = publickey;
            IV = iv;
            Paramters = parameters;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SimpleNetwork.Package.Packages.Internal.Secure
{
    [Serializable]
    public class ServerSecurePackage : IPackage
    {
        public byte[] PublicKey { get; protected set; }

        public byte[] IV { get; protected set; }

        public ServerSecurePackage(byte[] publickey,byte[] iv)
        {
            PublicKey = publickey;
            IV = iv;
        }
    }
}

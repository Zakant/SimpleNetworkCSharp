using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Secure
{
    [Serializable]
    public class CryptoPackage : IPackage
    {
        public byte[] Data { get; protected set; }

        public CryptoPackage(byte[] data)
        { Data = data; }

    }
}

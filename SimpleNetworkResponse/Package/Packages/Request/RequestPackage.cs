using SimpleNetwork.Client;
using SimpleNetwork.Package.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Request
{
    [Serializable]
    public abstract class RequestPackage : IPackage
    {
        [InsertId]
        public long ID { get; private set; }

        [InsertClient]
        public IClient Client { get; private set; }
    }
}

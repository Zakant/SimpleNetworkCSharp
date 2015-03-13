using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Response
{
    [Serializable]
    public abstract class ResponsePackage : IPackage
    {
        public ulong ResponseId { get; set; }

    }
}

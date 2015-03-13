using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Request
{
    [Serializable]
    public class RequestWrapper : RequestPackage
    {
        public IPackage Package { get; set; }

        public RequestWrapper(IPackage package)
        {
            Package = package;
        }
    }
}

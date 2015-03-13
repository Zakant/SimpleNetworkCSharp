using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Response
{
    [Serializable]
    public class ResponseWrapper : ResponsePackage
    {
        public IPackage Package { get; protected set; }

        public ResponseWrapper(IPackage package)
        {
            Package = package;
        }

    }
}

using SimpleNetwork.Client;
using SimpleNetwork.Package.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    public static class ClientExtensions
    {
        public static IPackageLog createLog(this IClient client)
        {
            Contract.Requires<ArgumentNullException>(client != null);
            return new PackageLog(client);
        }

    }
}

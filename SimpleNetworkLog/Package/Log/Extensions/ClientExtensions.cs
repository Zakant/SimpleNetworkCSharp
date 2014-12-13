using SimpleNetwork.Client;
using SimpleNetwork.Package.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    public static class ClientExtensions
    {
        public static IPackageLog createLog(this IClient client)
        {
            return new PackageLog(client);
        }

    }
}

using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    public static class ServerExtensions
    {
        public static IServerPackageLog createLog(this IServer server)
        {
            return new ServerPackageLog(server);
        }
    }
}

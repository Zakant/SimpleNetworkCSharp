using SimpleNetwork.Client;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Server
{
    public interface IServerLogEntry
    {
        IServer Server { get; }
        IClient Client { get; }
        IPackageLog Log { get; }
    }
}

using SimpleNetwork.Client;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries.Server
{
    [Serializable]
    public interface IServerLogEntry
    {
        [NonSerialized]
        IServer Server { get; }

        [NonSerialized]
        IClient Client { get; }

        IPackageLog Log { get; }
    }
}

using SimpleNetwork.Client;
using SimpleNetwork.Package.Log.Entries;
using SimpleNetwork.Package.Log.Entries.Server;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    [Serializable]
    public interface IServerPackageLog
    {
        [NonSerialized]
        IServer Server { get; }

        bool keepOnDisconnect { get; set; }

        IEnumerable<IServerLogEntry> Logs { get; }

        IPackageLog getLogForClient(IClient client);

        void addPackageLog(IPackageLog log);
    }
}

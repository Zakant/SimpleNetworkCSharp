using SimpleNetwork.Client;
using SimpleNetwork.Package.Log.Entries;
using SimpleNetwork.Package.Log.Entries.Client;
using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    [Serializable]
    public class PackageLog : IPackageLog
    {
        public IClient Client { get; protected set; }

        public IEnumerable<IPackageLogEntry> AllPackages { get { return new List<IPackageLogEntry>(packages); } }

        protected List<IPackageLogEntry> packages = new List<IPackageLogEntry>();


        public PackageLog(IClient client)
        {
            Client = client;
            client.NewMessage += (s, e) =>
                {
                    addPackage(e.Package, PackageOrigin.Remote);
                };
            client.MessageSend += (s, e) =>
                {
                    addPackage(e.Package, PackageOrigin.Local);
                };
        }

        public IEnumerable<ITypedPackageLogEntry<T>> getPackages<T>() where T : IPackage
        {
            return packages.Where(x => x.Package.GetType() == typeof(T)).Select(x => (new TypedPackageLogEntry<T>((T)x.Package, x.Origin, x.TimeStamp)) as ITypedPackageLogEntry<T>);
        }

        public void addPackage<T>(T package, PackageOrigin origin) where T : IPackage
        {
            packages.Add(new PackageLogEntry(package, origin));
        }
    }
}

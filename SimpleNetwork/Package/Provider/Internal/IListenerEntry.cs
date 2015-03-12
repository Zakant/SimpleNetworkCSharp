using SimpleNetwork.Package.Listener;
using System;

namespace SimpleNetwork.Package.Provider.Internal
{
    internal interface IListenerEntry
    {
        bool AcceptSubType { get; }

        Type AcceptType { get; }

        IPackageListener Listener { get; }

        bool ExclusivListener { get; }
    }
}

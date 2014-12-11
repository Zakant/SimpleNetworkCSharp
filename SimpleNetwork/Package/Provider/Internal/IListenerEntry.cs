using System;

namespace SimpleNetwork.Package.Provider.Internal
{
    internal interface IListenerEntry
    {
        bool AcceptSubType { get; }

        Type AcceptType { get; }

        object Listener { get; }

        bool ExclusivListener { get; }
    }
}

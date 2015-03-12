using SimpleNetwork.Package.Listener;
using System;

namespace SimpleNetwork.Package.Provider.Internal
{
    internal class ListenerEntry : IListenerEntry
    {
        public bool AcceptSubType { get; protected set; }

        public Type AcceptType { get; protected set; }

        public IPackageListener Listener { get; protected set; }

        public bool ExclusivListener { get; protected set; }

        public ListenerEntry(IPackageListener listener, Type acceptType) : this(listener, acceptType, false) { }

        public ListenerEntry(IPackageListener listener, Type acceptType, bool acceptSubType) : this(listener, acceptType, acceptSubType, false) { }

        public ListenerEntry(IPackageListener listener, Type acceptType, bool acceptSubType, bool isExclusiv)
        {
            AcceptSubType = acceptSubType;
            AcceptType = acceptType;
            Listener = listener;
        }
    }
}

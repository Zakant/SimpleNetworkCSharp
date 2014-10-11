using SimpleNetwork.Detection.Data;
using SimpleNetwork.Events;
using SimpleNetwork.Events.Secure;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Packages.Internal;
using SimpleNetwork.Package.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNetwork.Client.Secure
{
    public class SecureClient : Client, ISecureClient
    {
        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        protected ConnectionStateChangedEventArgs RaiseConnectionStateChanged(ConnectionState Old,ConnectionState New)
        {
            var myevent = ConnectionStateChanged;
            var args = new ConnectionStateChangedEventArgs(Old, New);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        protected Queue<IPackage> queue = new Queue<IPackage>();

        public byte[] PublicKey { get; protected set; }

        public byte[] PrivateKey { get; protected set; }

        private ConnectionState _state = ConnectionState.Undefined;
        public ConnectionState State
        {
            get { return _state; }
            set
            {
                var old = _state;
                State = value;
                RaiseConnectionStateChanged(old, State);
            }
        }

        protected override void PrepareConnection()
        {
            base.PrepareConnection();
            State = ConnectionState.Unsecure;
        }

        public override void SendPackage(IPackage package)
        {
            if (State != ConnectionState.Secure)
                queue.Enqueue(package);
            else
                base.SendPackage(package);
        }

        protected void SendUnsecure(IPackage package)
        {
            base.SendPackage(package);
        }
    }
}

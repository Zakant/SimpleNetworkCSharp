using SimpleNetwork.Client;
using SimpleNetwork.Events;
using SimpleNetwork.Package.Listener;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Packages.Internal;
using SimpleNetwork.Package.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleNetwork.Server
{
    public class Server : PackageProvider, IServer
    {

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        protected ClientConnectedEventArgs RaiseClientConnected(IClient c)
        {
            var myevent = ClientConnected;
            var args = new ClientConnectedEventArgs(c);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        protected ClientDisconnectedEventArgs RaiseClientDisconnected(IClient c, DisconnectReason reason)
        {
            var myevent = ClientDisconnected;
            var args = new ClientDisconnectedEventArgs(c, reason);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        private List<IClient> _clients = new List<IClient>();
        public IEnumerable<IClient> Clients
        {
            get { return new List<IClient>(_clients); }
        }

        public IPEndPoint EndPoint { get; protected set; }

        public int Port { get; set; }

        public bool AcceptNew { get; set; }

        public IClientFactory ClientFactory { get; set; }

        private bool isRunning = false;
        private TcpListener tcpserver;


        public Server(int port)
        {
            this.Port = port;
            ClientFactory = new ClientFactory();
            this.AcceptNew = true;
        }

        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                EndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, Port);
                tcpserver = new TcpListener(EndPoint);
                tcpserver.Start();
                RunAccepting();
            }
        }


        private void RunAccepting()
        {
            tcpserver.BeginAcceptTcpClient(new AsyncCallback(x =>
            {
                if (AcceptNew)
                {
                    var ic = ClientFactory.createFromTcpClient(tcpserver.EndAcceptTcpClient(x)); // Das TcpClient Object besorgen und durch die Fabrik jagen
                    var r = RaiseClientConnected(ic); // Mitteilen, dass es einen neune Client gibt
                    _clients.Add(ic);
                    ic.NewMessage += new EventHandler<NewMessageEventArgs>((sender, args) =>
                    {
                        RaiseNewMessage(args.Package, args.Client);
                        informListener(args.Package, args.Client);
                    });
                    ic.Disconnected += new EventHandler<DisconnectedEventArgs>((sender, args) =>
                    {
                        _clients.Remove(sender as IClient);
                        RaiseClientDisconnected(sender as IClient, args.Reason);
                    });
                    ic.StartListening();
                }
                if (isRunning)
                    RunAccepting();

            }), null);
        }

        public void Stop()
        {
            isRunning = false;
            foreach (var c in _clients)
                c.Disconnect();
        }

        public void BroadcastPackage(IPackage package)
        {
            foreach (var c in Clients)
                c.SendPackage(package);
        }
    }
}

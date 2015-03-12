using SimpleNetwork.Client;
using SimpleNetwork.Events;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Provider;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace SimpleNetwork.Server
{
    /// <summary>
    /// Stellt eine Serverklasse zum aufnehmen von Verbindungen mit mehreren Remotehosts da.
    /// </summary>
    public class Server : PackageProvider, IServer
    {

        /// <summary>
        ///  Tritt ein, wenn einer neuer Remotehost verbindet.
        /// </summary>
        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        /// <summary>
        /// Löst das <see cref="ClientConnected"/> Ereignis aus.
        /// </summary>
        /// <param name="c">Der neu verbundene Remotehost.</param>
        /// <returns>Die verwendeten Ereignis Argumente.</returns>
        protected ClientConnectedEventArgs RaiseClientConnected(IClient c)
        {
            var myevent = ClientConnected;
            var args = new ClientConnectedEventArgs(c);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        /// <summary>
        /// Tritt ein, wenn die Verbindung zu einem Remotehost abbricht.
        /// </summary>
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        /// <summary>
        /// Löst das <see cref="ClientDisconnected"/> Ereignis aus.
        /// </summary>
        /// <param name="c">Der Remotehost, zu dem die Verbindung verloren gegangen ist.</param>
        /// <param name="reason">Der Grund für den Verbindungsabbruch.</param>
        /// <returns>Die verwendeten Ereignis Argumente.</returns>
        protected ClientDisconnectedEventArgs RaiseClientDisconnected(IClient c, DisconnectReason reason)
        {
            var myevent = ClientDisconnected;
            var args = new ClientDisconnectedEventArgs(c, reason);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        private List<IClient> _clients = new List<IClient>();
        /// <summary>
        /// Stellt eine Auflistung aller verbundenen Remotehosts da.
        /// </summary>
        public ICollection<IClient> Clients
        {
            get { return new List<IClient>(_clients); }
        }

        /// <summary>
        /// Stellt den Endpunkt da, an dem der Server auf eingehende Verbindungsanfragen lauscht.
        /// </summary>
        public IPEndPoint EndPoint { get; protected set; }

        /// <summary>
        /// Stellt den Port da, auf dem der Server auf eigehnende Verbindungsanfragen lauscht.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Stellt einen Wert da, der angibt, ob der Server neue Verbindungsanfragen annimmt, oder ablehnt.
        /// </summary>
        public bool AcceptNew { get; set; }

        /// <summary>
        /// Stellt die IClientFactory da, mitderen Hilfe der Server neue IClient-Objekte erzeugt.
        /// </summary>
        public IClientFactory ClientFactory { get; set; }

        private bool isRunning = false;
        private TcpListener tcpserver;

        /// <summary>
        /// Initialisiert eine neue Instanz der Server-Klasse unter verwendung des angegebenen Ports.
        /// </summary>
        /// <param name="port">Der zu verwendene Port.</param>
        public Server(int port)
        {
            this.Port = port;
            ClientFactory = new ClientFactory();
            this.AcceptNew = true;
        }

        /// <summary>
        /// Starte den Sever.
        /// </summary>
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
                    ic.MessageIn += new EventHandler<MessageInEventArgs>((sender, args) =>
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

        /// <summary>
        /// Stop den Server.
        /// </summary>
        public void Stop()
        {
            isRunning = false;
            var _c = new List<IClient>(_clients);
            foreach (var c in _c)
                c.Disconnect();
        }

        /// <summary>
        /// Sendet ein Packet an alle verbundenen Remotehosts.
        /// </summary>
        /// <param name="package"></param>
        public void BroadcastPackage(IPackage package)
        {
            foreach (var c in Clients)
                c.SendPackage(package);
        }

        /// <summary>
        /// Gibt diese Server Instance und alle verwendeten Resourcen frei.
        /// </summary>
        public virtual void Dispose()
        {
            Stop();
        }
    }
}

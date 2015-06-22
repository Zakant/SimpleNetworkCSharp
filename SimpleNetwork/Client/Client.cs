using SimpleNetwork.Detection.Data;
using SimpleNetwork.Events;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Packages.Handshake;
using SimpleNetwork.Package.Packages.Internal;
using SimpleNetwork.Package.Provider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimpleNetwork.Client
{
    /// <summary>
    /// Stellt einen Clientklasse zum Aufnehmen von Verbindungen bereit.
    /// </summary>
    public class Client : PackageProvider, IClient
    {

        /// <summary>
        /// Tritt ein, wenn der Client die Verbindung zum Remotehost verliert.
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Löst das <see cref="Client.Disconnected"/> Ereignis aus.
        /// </summary>
        /// <param name="reason">Der Grund für den Verbindungsabbruch.</param>
        /// <returns>Die Verwendeten <see cref="DisconnectedEventArgs"/>.</returns>
        protected DisconnectedEventArgs RaiseDisconnected(DisconnectReason reason)
        {
            var myevent = Disconnected;
            var args = new DisconnectedEventArgs(reason);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        private TcpClient _client;

        private BinaryFormatter _formatter = new BinaryFormatter();

        /// <summary>
        /// Gibt an, ob das Client-Objekt eine aktive Verbindung besitzt.
        /// </summary>
        public bool isConnected
        {
            get { return _client != null && _client.Connected; }
        }

        /// <summary>
        /// Gibt den Localen Endpunkt an, an den dieser Client gebunden ist.
        /// </summary>
        public IPEndPoint LocalEndPoint
        {
            get
            {
                return (IPEndPoint)_client.Client.LocalEndPoint;
            }
        }

        /// <summary>
        /// Gitb den Remote Endpunkt an, zu dem dieser Client verbunden ist.
        /// </summary>
        public IPEndPoint RemoteEndPoint
        {
            get
            {
                return (IPEndPoint)_client.Client.RemoteEndPoint;
            }
        }

        private bool _isHandshakeCompleted = false;
        /// <summary>
        /// Gibt an, ob der Server-Client Handschlag abgeschlossen wurde.
        /// </summary>
        public bool isHandshakeCompleted
        {
            get { return _isHandshakeCompleted; }
            protected set
            {
                _isHandshakeCompleted = value;
                ResendPackages();
            }
        }

        /// <summary>
        /// Gibt an, ob der Client alle empfangene und gesendeten Packages speichern soll.
        /// </summary>
        public bool logPackageHistory { get; set; }

        /// <summary>
        /// Gibt an, ob es sich um einen Client handelt der als Teil eines Servers arbeitet oder nicht.
        /// </summary>
        public bool isServerClient { get; set; }

        /// <summary>
        /// Gibt an, ob der Client aktuell läuft.
        /// </summary>
        public bool isRunning { get; protected set; }

        /// <summary>
        /// Der Eingabestrom, aus dem die Pakete gelesen werden.
        /// </summary>
        protected Stream InStream { get; set; }

        /// <summary>
        /// Der Ausgabestrom, in den die Pakete geschrieben werden.
        /// </summary>
        protected Stream OutStream { get; set; }

        /// <summary>
        /// Initialisiert ein neues leeres Client-Objekt.
        /// </summary>
        public Client()
        {
            _client = new TcpClient();
            logPackageHistory = false;
        }

        /// <summary>
        /// Initialisiert ein neues Clinet-Objekt auf der Grundlage des TcpClient-Objektes.
        /// </summary>
        /// <param name="c">Das TcpClient-Objekt, dass Zugrunde liegt.</param>
        public Client(TcpClient c)
        {
            logPackageHistory = false;
            isServerClient = true;
            _client = c;
            SetUpConnection();
        }

        /// <summary>
        /// Verbindet mit einem Remote Server unter verwendunge der angegebenen IPAdresse und dem Port.
        /// </summary>
        /// <param name="ip">Die zu verwendene IPAdresse.</param>
        /// <param name="port">Der zu verwendene Port.</param>
        public void Connect(System.Net.IPAddress ip, int port)
        {
            _client.Connect(new IPEndPoint(ip, port));
            SetUpConnection();
        }

        /// <summary>
        /// Verbindet mit einem Remotehost unter verwendung des angegeben HostData-Objekst.
        /// </summary>
        /// <param name="data">Das HostData Objekt, zu dem die Verbindung aufgebaut werden soll.</param>
        public void Connect(HostData data)
        {
            Ping p = new Ping();
            foreach (var ip in data.Addresses)
            {
                if (p.Send(ip).Status == IPStatus.Success)
                {
                    Connect(ip, data.Port);
                    break;
                }
            }

        }

        /// <summary>
        /// Wird aufgerufen um die bestehende Verbindung mit einem Remotehost auf die Übertragung vorzubereiten.
        /// </summary>
        protected virtual void SetUpConnection()
        {
            InStream = _client.GetStream();
            OutStream = _client.GetStream();
            RemoveTypeListener<ShutDownPackage>();
            RegisterPackageListener<ShutDownPackage>((p, c) =>
            {
                Disconnect(DisconnectReason.ClosedProperly);
            });

            if (isServerClient)
            {
                RemoveTypeListener<ClientHandshakePackage>();
                RegisterPackageListener<ClientHandshakePackage>((p, c) =>
                    {
                        var serverHandshake = new ServerHandshakePackage();
                        PrepareServerAnswerHandshake(p, serverHandshake);
                        SendPackage(serverHandshake, true, true);
                        isHandshakeCompleted = true;
                        OpenConnection();
                    });
            }
            else
            {
                RemoveTypeListener<ServerHandshakePackage>();
                RegisterPackageListener<ServerHandshakePackage>((p, c) =>
                    {
                        HandleServerAnswerHandshake(p);
                        isHandshakeCompleted = true;
                        OpenConnection();
                    });
                var clientHandshake = new ClientHandshakePackage();
                PrepareClientHandshake(clientHandshake);
                SendPackage(clientHandshake, true, true);
            }
            StartListening();
        }

        /// <summary>
        /// Wird aufgerufen wenn der Client-Server Handschlag abgeschlossen wurde.
        /// </summary>
        protected virtual void OpenConnection()
        {

        }

        /// <summary>
        /// Wird aufgerufen, um das Client Paket des Client-Server Handschlages zu erzeugen.
        /// </summary>
        /// <param name="outClientHandshake">Das Client Paket des Handschlages, dass an den Server gesendet wird.</param>
        protected virtual void PrepareClientHandshake(ClientHandshakePackage outClientHandshake)
        {
            outClientHandshake.KeyValues.Add("OS", Environment.OSVersion.VersionString);
        }
        /// <summary>
        /// Wird aufgerufen, um das Server Paket des Client-Server Handschlages zu erzeugen.
        /// </summary>
        /// <param name="inClientHandshake">Das Client Paket des Handschlages, dass empfangen wurde.</param>
        /// <param name="outServerHandshake">Das Server Paket des Handschlages, dass an den Client gesendet wird.</param>
        protected virtual void PrepareServerAnswerHandshake(ClientHandshakePackage inClientHandshake, ServerHandshakePackage outServerHandshake)
        {
            outServerHandshake.KeyValues.Add("OS", Environment.OSVersion.VersionString);
        }
        /// <summary>
        /// Wird aufgerufen, um das Server Paket des Client-Server Handschlages zu verarbeiten.
        /// </summary>
        /// <param name="inServerHandshake">Das Server Paket des Handschlages, dass empfangen wurde.</param>
        protected virtual void HandleServerAnswerHandshake(ServerHandshakePackage inServerHandshake)
        {

        }

        /// <summary>
        /// Gibt an, ob zum aktuellen Zeitpunkt ein Verbindungtrennungs Vorgang läuft.
        /// </summary>
        protected bool isDisconnecting = false;

        /// <summary>
        /// Trennt die Verbindung zu dem Remotehost, und benarchtigt diesen über den Verbungsabbau.
        /// </summary>
        public void Disconnect()
        {
            SendPackage(new ShutDownPackage("Shut Down"));
            Disconnect(DisconnectReason.ClosedProperly);
        }

        /// <summary>
        /// Trennt die Verbindung zu dem Remotehost, mit der angegeben Begründung.
        /// </summary>
        /// <param name="r">Der Grund für den Verbindungsabbau- oder abbruch.</param>
        protected void Disconnect(DisconnectReason r)
        {
            if (!isDisconnecting)
            {
                isDisconnecting = true;
                if (isConnected)
                {
                    _client.Close();
                    RaiseDisconnected(r);
                }
                else if (!isConnected && isRunning)
                {
                    RaiseDisconnected(DisconnectReason.LostConnection);
                }
                StopListening();
                isDisconnecting = false;
            }
        }


        private object _lock = new object();
        private Queue<IPackage> _queue = new Queue<IPackage>();

        /// <summary>
        /// Sendet ein Paket an den Remotehost.
        /// </summary>
        /// <param name="package">Das zu sendene Paket.</param>
        public virtual void SendPackage(IPackage package)
        {
            SendPackage(package, false, false);
        }

        /// <summary>
        /// Sendet ein Paket an den Remotehost. Erlaubt es den Client-Server Handschlag sowie das <see cref="SimpleNetwork.Package.Provider.PackageProvider.MessageOut"/>-Event zu uebergeghen.
        /// </summary>
        /// <param name="package">Das zu sendene Paket.</param>
        /// <param name="bypassHandshake">Gibt an, ob der Client-Sever Handschlag ignoriert werden soll.</param>
        /// <param name="bypassMessageOut">Gitb an, ob das <see cref="SimpleNetwork.Package.Provider.PackageProvider.MessageOut"/>-Event ignoriert werden soll.</param>
        protected void SendPackage(IPackage package, bool bypassHandshake, bool bypassMessageOut)
        {
            try
            {
                lock (_lock)
                {
                    if (!(isHandshakeCompleted || bypassHandshake))
                        _queue.Enqueue(package);
                    else
                    {
                        ApplySendLogic(package);
                        if (bypassMessageOut || !RaiseSendMessage(package, this).Handled)
                            _formatter.Serialize(OutStream, transformPackageForSend(package));
                    }
                }
            }
            catch (ArgumentException)
            {
                Disconnect(DisconnectReason.LostConnection);
            }
            catch (IOException)
            {
                Disconnect(DisconnectReason.LostConnection); // Fehler, Verbindung verloren
            }
        }

        private void ResendPackages()
        {
            if (_queue.Count == 0 || !isHandshakeCompleted) return; // Wenn es keine Pakete gibt, koennen wir aufhoeren, oder wenn der handschlag nicht fertig ist.
            foreach (var p in _queue.ToArray()) // Alle Pakete in ein neues Array und durchloopen.
            {
                SendPackage(p); // Jedes Paket versenden.
            }
            ResendPackages(); // Und rekursiv aufrufen, um sicherzustellen, dass wirklich alle Pakete gesendet wurden.
        }

        /// <summary>
        /// Transformiert ein Paket so, dass es an den Remotehost gesendet werden kann.
        /// </summary>
        /// <param name="package">Das urspruengliche Paket.</param>
        /// <returns>Das transformierte Paket.</returns>
        protected virtual IPackage transformPackageForSend(IPackage package)
        {
            return package;
        }

        /// <summary>
        /// Beginnt eigehende Pakete zu empfangen und zu verarbeiten.
        /// </summary>
        public void StartListening()
        {
            if (!isRunning)
            {
                isRunning = true;
                RunListenting();
            }
        }

        private void RunListenting()
        {
            var listenting = new Func<IPackage>(() => readFromStream());
            listenting.BeginInvoke(new AsyncCallback(x =>
                {
                    if (isRunning)
                    {
                        var r = listenting.EndInvoke(x);
                        if (r == null)
                        {
                            Disconnect(DisconnectReason.Unknown);
                        }
                        else
                        {
                            HandleNewMessage(r);
                            if (isConnected)
                                RunListenting();
                        }
                    }
                }), null);
        }

        /// <summary>
        /// Stoppt eigehende Pakete zu empfangen und zu verarbeiten.
        /// </summary>
        public void StopListening()
        {
            isRunning = false;
        }

        /// <summary>
        /// List das neuste Paket aus dem <see cref="Client.InStream"/>.
        /// </summary>
        /// <returns>Das neuste gelesene Paket.</returns>
        protected IPackage readFromStream()
        {
            try
            {
                IPackage p = (_formatter.Deserialize(InStream) as IPackage);
                return p;
            }
            catch (Exception) // Hier kann auch das Disconnect festgestellt werden!
            {
                return null;
            }
        }

        /// <summary>
        /// Wird für jedes neue Paket aufgerufen um es weiterzuverarbeiten.
        /// </summary>
        /// <param name="p">Das zuverarbeitende Paket.</param>
        protected virtual void HandleNewMessage(IPackage p)
        {
            ApplyReceiveLogic(p);
            if (!RaiseNewMessage(p, this).Handled)
                informListener(p, this);
        }

        /// <summary>
        /// Gitb diese Client Instanz und alle verwendeten Resourcen frei.
        /// </summary>
        public virtual void Dispose()
        {
            Disconnect();
        }


        private void ApplySendLogic(IPackage package)
        {

        }

        private void ApplyReceiveLogic(IPackage package)
        {

        }

    }
}

using SimpleNetwork.Events;
using SimpleNetwork.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Listener;
using SimpleNetwork.Package.Provider;
using SimpleNetwork.Package.Packages.Internal;
using SimpleNetwork.Detection;
using System.Net.NetworkInformation;
using SimpleNetwork.Detection.Data;

namespace SimpleNetwork.Client
{
    /// <summary>
    /// Stellt einen Clientklasse zum aufnehmen von Verbindungen bereit
    /// </summary>
    public class Client : PackageProvider, IClient
    {

        /// <summary>
        /// Tritt ein, wenn der Client die Verbindung zum Remotehost verliert
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Löst das <see cref="Client.Disconnected"/> Ereignis aus.
        /// </summary>
        /// <param name="reason">Der Grund für den Verbindungsabbruch</param>
        /// <returns>Die Verwendeten <see cref="DisconnectedEventArgs"/></returns>
        protected DisconnectedEventArgs RaiseDisconnected(DisconnectReason reason)
        {
            var myevent = Disconnected;
            var args = new DisconnectedEventArgs(reason);
            if (myevent != null)
                myevent(this, args);
            return args;
        }

        private TcpClient _client;

        private bool isrunning = false;

        private NetworkStream _networkstream;
        private BinaryFormatter _formatter = new BinaryFormatter();

        /// <summary>
        /// Gibt an, ob das Client-Objekt eine aktive Verbindung besitzt. True fals ja, False, false nein.
        /// </summary>
        public bool isConnected
        {
            get { return _client.Connected; }
        }


        /// <summary>
        /// Gibt an, ob der Client alle empfangene und gesendeten Packages speichern soll.
        /// </summary>
        public bool logPackageHistory { get; set; }

        /// <summary>
        /// Initialisiert ein neues leeres Client-Objekt
        /// </summary>
        public Client()
        {
            logPackageHistory = false;
        }

        /// <summary>
        /// Initialisiert ein neues Clinet-Objekt auf der Grundlage des TcpClient-Objektes
        /// </summary>
        /// <param name="c">Das TcpClient-Objekt, dass Zugrunde liegt</param>
        public Client(TcpClient c)
        {
            logPackageHistory = false;
            _client = c;
            PrepareConnection();
        }

        /// <summary>
        /// Verbindet mit einem Remote Server unter verwendunge der angegebenen IPAdresse und dem Port
        /// </summary>
        /// <param name="ip">Die zu verwendene IPAdresse</param>
        /// <param name="port">Der zu verwendene Port</param>
        public void Connect(System.Net.IPAddress ip, int port)
        {
            _client = new TcpClient();
            _client.Connect(new IPEndPoint(ip, port));
            PrepareConnection();
            StartListening();
        }

        /// <summary>
        /// Verbindet mit einem Remote Server unter verwendung des angegeben HostData-Objekst
        /// </summary>
        /// <param name="data">Das HostData Objekt, zu dem die Verbindung aufgebaut werden soll</param>
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

        protected void PrepareConnection()
        {
            _networkstream = _client.GetStream();
            RemoveTypeListener<ShutDownPackage>();
            RegisterPackageListener<ShutDownPackage>((p, c) =>
            {
                Disconnect(DisconnectReason.ClosedProperly);
            });
        }

        protected bool isDisconnecting = false;

        /// <summary>
        /// Trennt die Verbindung zu dem Remotehost, und benarchtigt diesen über den Verbungsabbau
        /// </summary>
        public void Disconnect()
        {
            SendPackage(new ShutDownPackage("Shut Down"));
            Disconnect(DisconnectReason.ClosedProperly);
        }

        /// <summary>
        /// Trennt die Verbindung zu dem Remotehost, mit der angegeben Begründung
        /// </summary>
        /// <param name="r">Der Grund für den Verbindungsabbau- oder abbruch</param>
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
                else if (!isConnected && isrunning)
                {
                    RaiseDisconnected(DisconnectReason.LostConnection);
                }
                StopListening();
                isDisconnecting = false;
            }
        }

        /// <summary>
        /// Sendet ein Packet an den Remotehost
        /// </summary>
        /// <param name="package">Das zu sendene Packet</param>
        public void SendPackage(IPackage package)
        {
            try
            {
                _formatter.Serialize(_networkstream, package);
            }
            catch (Exception)
            {
                Disconnect(DisconnectReason.LostConnection); // Fehler, verbindung verloren
            }

        }

        /// <summary>
        /// Beginnt auf eingehende Packete zu lauschen
        /// </summary>
        public void StartListening()
        {
            isrunning = true;
            RunListenting();
        }

        private void RunListenting()
        {
            var listenting = new Func<IPackage>(() => readFromStream());
            listenting.BeginInvoke(new AsyncCallback(x =>
                {
                    if (isrunning)
                    {
                        var r = listenting.EndInvoke(x);
                        if (r == null)
                        {
                            Disconnect(DisconnectReason.Unknown);
                        }
                        else
                        {
                            RaiseNewMessage(r, this);
                            informListener(r, this);
                            if (isConnected)
                                RunListenting();
                        }
                    }
                }), null);
        }

        /// <summary>
        /// Stoppt auf eigehende Pakete zu lauschen
        /// </summary>
        public void StopListening()
        {
            isrunning = false;
        }

        protected IPackage readFromStream()
        {
            try
            {
                return (_formatter.Deserialize(_networkstream) as IPackage);
            }
            catch (Exception) // Hier kann auch das Disconnect festgestellt werden!
            { return null; }
        }

    }
}

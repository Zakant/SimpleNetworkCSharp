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
        /// Tritt ein, wenn der Client die Verbindung zum Remoteclient verliert
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Löst das Event "Disconnected" aus
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
        /// 
        /// </summary>
        public bool isConnected
        {
            get { return _client.Connected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Client()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        public Client(TcpClient c)
        {
            _client = c;
            PrepareConnection();
        }

        public void Connect(System.Net.IPAddress ip, int port)
        {
            _client = new TcpClient();
            _client.Connect(new IPEndPoint(ip, port));
            PrepareConnection();
            StartListening();
        }

        public void Connect(HostData data)
        {
            Ping p = new Ping();
            foreach(var ip in data.Addresses)
            {
                if (p.Send(ip).Status == IPStatus.Success)
                {
                    Connect(ip, data.Port);
                    break;
                }
            }
            
        }

        private void PrepareConnection()
        {
            _networkstream = _client.GetStream();
            RemoveTypeListener<ShutDownPackage>();
            RegisterPackageListener<ShutDownPackage>((p, c) =>
            {
                Disconnect(DisconnectReason.ClosedProperly);
            });
        }

        private bool isDisconnecting = false;
        public void Disconnect()
        {
            SendPackage(new ShutDownPackage("Shut Down"));
            Disconnect(DisconnectReason.ClosedProperly);
        }

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

        public void StopListening()
        {
            isrunning = false;
        }

        private IPackage readFromStream()
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

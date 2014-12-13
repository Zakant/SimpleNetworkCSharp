using SimpleNetwork.Detection.Data;
using SimpleNetwork.Server;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SimpleNetwork.Detection.Announcer
{
    /// <summary>
    /// Ermöglicht das aufspüren eines Servers im Lokalen Netzwerk.
    /// </summary>
    public class PushNetworkAnnouncer : NetworkAnnouncerBase
    {

        private UdpClient _receiveClient;
        private UdpClient _sendClient;
        private IPEndPoint _allpoint;
        private byte[] _data;

        /// <summary>
        /// Erstellt ein neues NetworkAnnouncer-Objekt auf Grundlage eines IServer-Objektes.
        /// </summary>
        /// <param name="server">Das zuverwende IServer-Objekt.</param>
        public PushNetworkAnnouncer(IServer server)
            : base(server)
        {
        }

        /// <summary>
        /// Starte das Beantworten von Anfragen.
        /// </summary>
        public override void StartAnnouncing()
        {
            if (!Announcing)
            {
                UpdateHostData();
                _allpoint = new IPEndPoint(IPAddress.Any, AnnouncingPort);
                _sendClient = new UdpClient();
                _receiveClient = new UdpClient(_allpoint);
                Announcing = true;
                _data = Data.toByteArray();
                RunListenting();
            }
        }


        private void RunListenting()
        {
            _receiveClient.BeginReceive(new AsyncCallback(x =>
                {
                    if (Announcing)
                    {
                        var r = _receiveClient.EndReceive(x, ref _allpoint);
                        var hd = HostData.fromByteArray(r);
                        Ping p = new Ping();
                        IPAddress a = null;
                        foreach (var ip in hd.Addresses)
                            if (p.Send(ip).Status == IPStatus.Success)
                            {
                                a = ip;
                                break;
                            }
                        if (a != null)
                        {
                            _sendClient.Send(_data, _data.Length, new IPEndPoint(a, hd.Port));
                        }
                        RunListenting();
                    }
                }), null);
        }

        /// <summary>
        /// Stopt das Beantworten von Anfragen.
        /// </summary>
        public override void StopAnnouncing()
        {
            if (Announcing)
            {
                _receiveClient.Close();
                _sendClient.Close();
                Announcing = false;
            }
        }
    }
}

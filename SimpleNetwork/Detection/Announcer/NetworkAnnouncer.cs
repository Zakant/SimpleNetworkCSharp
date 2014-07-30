using SimpleNetwork.Detection.Data;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace SimpleNetwork.Detection.Announcer
{
    public class NetworkAnnouncer : NetworkAnnouncerBase
    {

        private Timer _sendtimer = null;
        private UdpClient _client = null;
        private byte[] _data;
        private IPEndPoint _endpoint;

        public NetworkAnnouncer(IServer server)
            : base(server)
        {
        }

        public override void StartAnnouncing()
        {
            if (!Announcing)
            {
                _sendtimer = new Timer(Intervall);
                _sendtimer.AutoReset = true;
                _client = new UdpClient();
                _endpoint = new IPEndPoint(IPAddress.Broadcast, AnnouncingPort);
                _data = Data.toByteArray();
                _sendtimer.Elapsed += new ElapsedEventHandler((sender, args) =>
                { // Hier wird announced
                    _client.Send(_data, _data.Length, _endpoint);
                });
                _sendtimer.Start();
                Announcing = true;
            }
        }

        public override void StopAnnouncing()
        {
            if (Announcing)
            {
                _sendtimer.Stop();
                _sendtimer.Dispose();
                _sendtimer = null;
                _client.Close();
                Announcing = false;
            }
        }
    }
}

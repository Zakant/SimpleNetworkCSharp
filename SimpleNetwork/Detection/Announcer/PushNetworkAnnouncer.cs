using SimpleNetwork.Detection.Data;
using SimpleNetwork.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace SimpleNetwork.Detection.Announcer
{
    public class PushNetworkAnnouncer : NetworkAnnouncerBase
    {

        private UdpClient _receiveClient;
        private UdpClient _sendClient;
        private IPEndPoint _allpoint;
        private byte[] _data;

        public PushNetworkAnnouncer(IServer server)
            : base(server)
        {
        }

        public override void StartAnnouncing()
        {
            if (!Announcing)
            {
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

using SimpleNetwork.Detection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleNetwork.Detection.Detector
{

    public class PushNetworkDetector : NetworkDetectorBase
    {


        private UdpClient _sendClient;
        private UdpClient _receiveClient;
        private IPEndPoint _allpoint;
        private IPEndPoint _broadcast;

        public int ReceivePort { get; set; }

        public PushNetworkDetector()
        {
            ReceivePort = 16000;
        }

        public override void StartDetection()
        {
            if (!isDetecting)
            {
                isDetecting = true;
                _allpoint = new IPEndPoint(IPAddress.Any, ReceivePort);
                _sendClient = new UdpClient();
                _receiveClient = new UdpClient(_allpoint);

                RunListening();
                Refresh();
                StartCleanUp();
            }
        }

        private void RunListening()
        {
            _receiveClient.BeginReceive(new AsyncCallback(x =>
                {
                    if (isDetecting)
                    {
                        var data = HostData.fromByteArray(_receiveClient.EndReceive(x, ref _allpoint));
                        if (!_hosts.Any(y => y.data.ID == data.ID))
                        {
                            _hosts.Add(new HostDataTime() { data = data, stamp = DateTime.Now });
                            RaiseHostFound(data);
                        }
                        RunListening();
                    }
                }), null);
        }

        public override void Refresh()
        {
            if (!isDetecting)
                StartDetection();
            Clear();
            _broadcast = new IPEndPoint(IPAddress.Broadcast, DetectionPort);
            byte[] data = (new HostData() { Addresses = GetIPAddress(), Port = ReceivePort }).toByteArray();
            _sendClient.Send(data, data.Length, _broadcast);
        }

        public override void StopDetection()
        {
            if (isDetecting)
            {
                isDetecting = false;
                StopCleanUp();
            }
        }
    }
}

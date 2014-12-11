using SimpleNetwork.Detection.Data;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace SimpleNetwork.Detection.Detector
{
    /// <summary>
    /// Ermöglicht das auffinden von Servern im lokalen Netzwerk.
    /// </summary>
    public class NetworkDetector : NetworkDetectorBase
    {

        private IPEndPoint _endpoint;
        private UdpClient _client;

        /// <summary>
        /// Beginnt mit der Erfassung von Servern.
        /// </summary>
        public override void StartDetection()
        {
            if (!isDetecting)
            {
                isDetecting = true;
                _client = new UdpClient(DetectionPort);
                _endpoint = new IPEndPoint(IPAddress.Any, DetectionPort);
                StartCleanUp();
                Refresh();
                RunListening();
            }
        }

        private void RunListening()
        {
            _client.BeginReceive(new AsyncCallback(x =>
                {
                    if (isDetecting)
                    {
                        var data = HostData.fromByteArray(_client.EndReceive(x, ref _endpoint));
                        lock (LockObject)
                        {
                            if (!_hosts.Any(y => y.data.ID == data.ID))
                            {
                                _hosts.Add(new HostDataTime() { data = data, stamp = DateTime.Now });
                                RaiseHostFound(data);
                            }
                            else
                                _hosts.First(y => y.data.ID == data.ID).stamp = DateTime.Now;
                        }
                        RunListening();
                    }
                }), null);
        }

        /// <summary>
        /// Aktualisert die vorhandenen Serverinformationen.
        /// </summary>
        public override void Refresh()
        {
            Clear();
            if (!isDetecting)
                StartDetection();
        }

        /// <summary>
        /// Beendet die Erfassung von Servern.
        /// </summary>
        public override void StopDetection()
        {
            if (isDetecting)
            {
                isDetecting = false;
                StopCleanUp();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            _client.Close();
        }
    }
}

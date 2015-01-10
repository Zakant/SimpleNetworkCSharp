using SimpleNetwork.Detection.Data;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace SimpleNetwork.Detection.Detector
{
    /// <summary>
    /// Ermöglicht das auffinden von Servern im lokalen Netzwerk.
    /// Dieser NetworkDetctor muss die Suche manuell starten.
    /// Hiefür muss die <see cref="Refresh"/> Methode aufgerufen werden. Ohne diesen Aufruf wird die Liste NICHT automatisch aktualisert.
    /// Beim Aufruf von <see cref="StartDetection"/> wird automatisch erstmalig <see cref="Refresh"/> aufgerufen.
    /// </summary>
    public class PushNetworkDetector : NetworkDetectorBase
    {


        private UdpClient _sendClient;
        private UdpClient _receiveClient;
        private IPEndPoint _allpoint;
        private IPEndPoint _broadcast;

        /// <summary>
        /// Der Port, auf dem die eingehenden Serverinformationen empfangen werden sollen.
        /// </summary>
        public int ReceivePort { get; set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der PushNetworkDetector-Klasse.
        /// </summary>
        public PushNetworkDetector()
        {
            ReceivePort = 16000;
        }

        /// <summary>
        /// Beginnt mit der Erfassung von Servern.
        /// Sollte keinen Server gefunden werden rufen sie manuell <see cref="Refresh"/> auf.
        /// </summary>
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
                        else
                            _hosts.First(y => y.data.ID == data.ID).stamp = DateTime.Now;
                        RunListening();
                    }
                }), null);
        }

        /// <summary>
        /// Aktualisiert die Serverinformationen.
        /// Sollte <see cref="NetworkDetectorBase.Hosts"/> den von ihnen gesuchten Host nicht enthalten, so kann eine erneute Suche durch den Aufruf dieser Funktion erreicht werden.
        /// </summary>
        public override void Refresh()
        {
            if (!isDetecting)
                StartDetection();
            Clear();
            _broadcast = new IPEndPoint(IPAddress.Broadcast, DetectionPort);
            byte[] data = (new HostData() { Addresses = GetIPAddress(), Port = ReceivePort }).toByteArray();
            _sendClient.Send(data, data.Length, _broadcast);
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

        /// <summary>
        /// Gibt alle verwendeten Resourcen frei.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            _sendClient.Close();
            _receiveClient.Close();
        }
    }
}

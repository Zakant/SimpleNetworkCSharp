﻿using SimpleNetwork.Detection.Data;
using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Provider;
using System;
using System.Net;

namespace SimpleNetwork.Client
{
    /// <summary>
    /// Stellt eine Objekt da, dass über das Netzwerk mit einem Remotehost verbunden ist
    /// </summary>
    public interface IClient : IPackageProvider, IDisposable
    {
        /// <summary>
        /// Tritt ein, wenn der Client die Verbindung zum Remotehost verliert
        /// </summary>
        event EventHandler<SimpleNetwork.Events.DisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Gibt an, ob das Client-Objekt eine aktive Verbindung besitzt. True fals ja, False, false nein.
        /// </summary>
        bool isConnected { get; }


        /// <summary>
        /// Gibt an, ob der Client alle empfangene und gesendeten Packages speichern soll.
        /// </summary>
        bool logPackageHistory { get; set; }

        /// <summary>
        /// Verbindet mit einem Remote Server unter verwendunge der angegebenen IPAdresse und dem Port
        /// </summary>
        /// <param name="ip">Die zu verwendene IPAdresse</param>
        /// <param name="port">Der zu verwendene Port</param>
        void Connect(IPAddress ip, int port);

        /// <summary>
        /// Verbindet mit einem Remote Server unter verwendung des angegeben HostData-Objekst
        /// </summary>
        /// <param name="data">Das HostData Objekt, zu dem die Verbindung aufgebaut werden soll</param>
        void Connect(HostData data);

        /// <summary>
        /// Trennt die Verbindung zu dem Remotehost, und benarchtigt diesen über den Verbungsabbau
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Beginnt auf eingehende Packete zu lauschen
        /// </summary>
        void StartListening();

        /// <summary>
        /// Stoppt auf eigehende Pakete zu lauschen
        /// </summary>
        void StopListening();

        /// <summary>
        /// Sendet ein Packet an den Remotehost
        /// </summary>
        /// <param name="package">Das zu sendene Packet</param>
        void SendPackage(IPackage package);

    }
}

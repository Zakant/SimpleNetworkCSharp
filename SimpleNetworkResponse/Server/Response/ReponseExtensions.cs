﻿using SimpleNetwork.Client;
using SimpleNetwork.Events;
using SimpleNetwork.Package.Packages.Request;
using SimpleNetwork.Package.Packages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Server.Response
{
    /// <summary>
    /// Stellt Erweiterungsmethoden zum Antworten auf Anfragen bereit.
    /// </summary>
    public static class ReponseExtensions
    {
        /// <summary>
        /// Sendet eine Antwort.
        /// </summary>
        /// <param name="request">Die Anfrage auf die geantwortet wird.</param>
        /// <param name="response">Das Antwort Paket.</param>
        public static void SendResponse(this RequestPackage request, ResponsePackage response)
        {
            if (request.Client == null) throw new ArgumentNullException("request.Client", "Client is null! Maybe you forgot to call 'server.EnableResponseSystem()'?!");
            response.ResponseId = request.ID;
            request.Client.SendPackage(response);
        }

        /// <summary>
        /// Aktiviert des Anfrage-Antwort System.
        /// </summary>
        /// <param name="server">Der Server für den das System aktiviert wird.</param>
        public static void EnableResponseSystem(this IServer server)
        {
            server.ClientConnected += HandleClientConenct;
            foreach (var c in server.Clients)
                c.EnableResponseSystem();
        }
        /// <summary>
        /// Aktiviert des Anfrage-Antwort System.
        /// </summary>
        /// <param name="client">Der Client für den das Sysmte aktiviert wird.</param>
        public static void EnableResponseSystem(this IClient client)
        {
            client.MessageIn += HandleMessageIn;
        }

        private static void HandleClientConenct(object sender, ClientConnectedEventArgs e)
        {
            e.Client.EnableResponseSystem();
        }

        private static void HandleMessageIn(object sender, MessageInEventArgs e)
        {
            if(e.Package is RequestPackage)
            {
                var p = (RequestPackage)e.Package;
                p.Client = e.Client;
            }
        }
    }
}

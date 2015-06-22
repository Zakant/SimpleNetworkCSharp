using SimpleNetwork.Events;
using SimpleNetwork.Package.Attributes;
using SimpleNetwork.Package.Extensions;
using SimpleNetwork.Package.Packages.Request;
using SimpleNetwork.Package.Packages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client.Request
{
    /// <summary>
    /// Stellt Erweiterungsmethoden bereit, um Anfragen zu erstellen und zu verwalten.
    /// </summary>
    public static class RequestExtensions
    {

        private static List<IClient> _registeredClients = new List<IClient>();

        private static Dictionary<ulong, IRequest> _requests = new Dictionary<ulong, IRequest>();

        /// <summary>
        /// Erstellt eine neue Anfrage.
        /// </summary>
        /// <typeparam name="TRequest">Der Typ des Anfrage Paketes.</typeparam>
        /// <param name="client">Der Client über die die Anfrage versendet wird.</param>
        /// <param name="package">Das Anfrage Paket.</param>
        /// <returns>Die Anfrage.</returns>
        public static Request<TRequest, ResponsePackage> createRequest<TRequest>(this IClient client, TRequest package) where TRequest : RequestPackage
        {
            return createRequest<TRequest, ResponsePackage>(client, package);
        }

        /// <summary>
        /// Erstellt eine neue Anfrage.
        /// </summary>
        /// <typeparam name="TResponse">Der Typ des Antwort Paketes.</typeparam>
        /// <param name="client">Der Client über die die Anfrage versendet wird.</param>
        /// <param name="package">Das Anfrage Paket.</param>
        /// <returns>Die Anfrage.</returns>
        public static Request<RequestPackage, TResponse> createRequest<TResponse>(this IClient client, RequestPackage package) where TResponse : ResponsePackage
        {
            return createRequest<RequestPackage, TResponse>(client, package);
        }

        /// <summary>
        /// Erstellt eine neue Anfrage.
        /// </summary>
        /// <typeparam name="TRequest">Der Typ des Anfrage Paketes.</typeparam>
        /// <typeparam name="TResponse">Der Typ des Antwort Paketes.</typeparam>
        /// <param name="client">Der Client über die die Anfrage versendet wird.</param>
        /// <param name="package">Das Anfrage Paket.</param>
        /// <returns>Die Anfrage.</returns>
        public static Request<TRequest, TResponse> createRequest<TRequest, TResponse>(this IClient client, TRequest package)
            where TRequest : RequestPackage
            where TResponse : ResponsePackage
        {
            if (!isClientRegistered(client))
                RegisterClient(client);

            package.ID = getID();
            // package.ReplaceValues(typeof(InsertIdAttribute), x => ((id == -1) ? (id = getID()) : getID()));
            id = package.ID;

            var request = new Request<TRequest, TResponse>(package, client);
            _requests.Add(id, request);
            return request;
        }


        private static void HandleReponse(object sender, MessageInEventArgs e)
        {
            if (e.Package is ResponsePackage)
            {
                var response = (ResponsePackage)e.Package;
                if (_requests.ContainsKey(response.ResponseId))
                    if (_requests[response.ResponseId].ResponseType.IsAssignableFrom(e.MessageType))
                    {
                        _requests[response.ResponseId].ReceiveResponse(response);
                        _requests.Remove(response.ResponseId);
                        e.Handled = true;
                    }
            }
        }

        private static void RegisterClient(IClient client)
        {
            client.MessageIn += HandleReponse;

            _registeredClients.Add(client);
        }

        private static bool isClientRegistered(IClient client)
        {
            return _registeredClients.Contains(client);
        }

        private static object idLock = new object();
        private static ulong id = 0;
        private static ulong getID()
        {
            lock (idLock)
                return id++;
        }
    }
}

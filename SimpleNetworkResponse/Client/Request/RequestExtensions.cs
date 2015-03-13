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
    public static class RequestExtensions
    {

        private static List<IClient> _registeredClients = new List<IClient>();

        private static Dictionary<ulong, IRequest> _requests = new Dictionary<ulong, IRequest>();

        public static Request<TRequest, ResponsePackage> createRequest<TRequest>(this IClient client, TRequest package) where TRequest : RequestPackage
        {
            return createRequest<TRequest, ResponsePackage>(client, package);
        }

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

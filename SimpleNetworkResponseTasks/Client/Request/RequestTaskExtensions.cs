using SimpleNetwork.Package.Packages.Request;
using SimpleNetwork.Package.Packages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNetwork.Client.Request
{
    /// <summary>
    /// Stellt Erweiterungsmethoden zum asynchronen senden von Anfragen bereit.
    /// </summary>
    public static class RequestTaskExtensions
    {
        /// <summary>
        /// Sendet eine Anfrage asynchron.
        /// </summary>
        /// <typeparam name="TRequest">Der Typ des Anfrage Paketes.</typeparam>
        /// <typeparam name="TResponse">Der Typ des Antwort Paketes.</typeparam>
        /// <param name="request">Das Anfrage Pakete.</param>
        /// <returns>Gibt <see cref="System.Threading.Tasks.Task{T}"/> zurück</returns>
        public static Task<TResponse> SendAsync<TRequest, TResponse>(this Request<TRequest, TResponse> request)
            where TRequest : RequestPackage
            where TResponse : ResponsePackage
        {
            return Task.Run<TResponse>(() =>
            {
                if (!request.isSend)
                    request.Send();
                request.WaitOne();
                return request.ResponsePackage;
            });
        }
    }
}

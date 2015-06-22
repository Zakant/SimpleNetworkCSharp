using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Events.Request
{
    /// <summary>
    /// Stellt Daten für das <see cref="SimpleNetwork.Client.Request.Request{TRequest,TResponse}.ResponseReceived"/> Ereignis bereit.
    /// </summary>
    /// <typeparam name="TResponse">Der Typ des Antwort Paketes.</typeparam>
    public class ResponseReceivedEventArgs<TResponse> : SimpleNetworkEventArgs
    {
        /// <summary>
        /// Das Antwort Paket.
        /// </summary>
        public TResponse Response { get; protected set; }

        /// <summary>
        /// Erstellt eine neue Instanz.
        /// </summary>
        /// <param name="response">Das Antwort Paket.</param>
        public ResponseReceivedEventArgs(TResponse response)
        {
            Response = response;
        }
    }
}

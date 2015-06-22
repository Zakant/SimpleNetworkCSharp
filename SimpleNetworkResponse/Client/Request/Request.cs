using SimpleNetwork.Events.Request;
using SimpleNetwork.Package.Packages.Request;
using SimpleNetwork.Package.Packages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SimpleNetwork.Client.Request
{
    /// <summary>
    /// Stellt eine Anfrage da.
    /// </summary>
    /// <typeparam name="TRequest">Der Typ des Anfrage Paketes.</typeparam>
    /// <typeparam name="TResponse">Der Typ des Antwort Paketes.</typeparam>
    public class Request<TRequest, TResponse> : IRequest
        where TRequest : RequestPackage
        where TResponse : ResponsePackage
    {

        /// <summary>
        /// Tritt ein, wenn eine Antwort empfangen wurde.
        /// </summary>
        public event EventHandler<ResponseReceivedEventArgs<TResponse>> ResponseReceived;

        /// <summary>
        /// Löst das <see cref="SimpleNetwork.Client.Request.Request{TRequest,TResponse}.ResponseReceived"/> Ereignis aus.
        /// </summary>
        /// <param name="response">Das Antwort Paket.</param>
        protected void RaiseResponseReceived(TResponse response)
        {
            var myevent = ResponseReceived;
            if (myevent != null)
                myevent(this, new ResponseReceivedEventArgs<TResponse>(response));
        }

        /// <summary>
        /// Der Typ des Anfrage Paketes.
        /// </summary>
        public Type RequestType { get { return typeof(TRequest); } }

        /// <summary>
        /// Der Typ des Antwort Paketes.
        /// </summary>
        public Type ResponseType { get { return typeof(TResponse); } }

        /// <summary>
        /// Das Anfrage Paket.
        /// </summary>
        public TRequest RequestPackage { get; protected set; }

        /// <summary>
        /// Das Antwort Paket.
        /// </summary>
        public TResponse ResponsePackage { get; protected set; }

        /// <summary>
        /// Der Client über den die Anfrage verschickt wird.
        /// </summary>
        public IClient Client { get; protected set; }

        /// <summary>
        /// Gibt an, ob die Anfrage bereits versendet wurde.
        /// </summary>
        public bool isSend { get; protected set; }

        /// <summary>
        /// Gibt an, ob die Anfrage sowieo Antwort bereits abgeschlossen wurden.
        /// </summary>
        public bool isCompleted { get; protected set; }

        private ManualResetEvent reset = new ManualResetEvent(false);

        /// <summary>
        /// Erstellt eine neue Anfrage.
        /// Rufen sie diesen Konstruktur niemals direkt auf! Verwenden sie statt dessen die Erweiterungsmethode <see cref="SimpleNetwork.Client.Request.RequestExtensions.createRequest{TRequest, TResponse}"/>
        /// </summary>
        /// <param name="request">Das Anfrage Paket.</param>
        /// <param name="client">Der Client über den die Anfrage verschickt wird.</param>
        public Request(TRequest request, IClient client)
        {
            RequestPackage = request;
            Client = client;
            isSend = false;
            isCompleted = false;
        }

        /// <summary>
        /// Sendet die Anfrage.
        /// </summary>
        public void Send()
        {
            Client.SendPackage(RequestPackage);
            isSend = true;
        }

        /// <summary>
        /// Teilt der Anfrage die zugehörige Antwort mit.
        /// </summary>
        /// <param name="response">Die Antwort zu der Anfrage.</param>
        public void ReceiveResponse(ResponsePackage response)
        {
            if (!(response is TResponse)) throw new ArgumentException("Response has invalid type!", "response");
            ResponsePackage = (TResponse)response;
            isCompleted = true;
            reset.Set();
            RaiseResponseReceived(ResponsePackage);
        }

        /// <summary>
        /// Wartet auf das Empfangen einer Antwort.
        /// </summary>
        public void WaitOne()
        {
            reset.WaitOne();
        }

    }
}

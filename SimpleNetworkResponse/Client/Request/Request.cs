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
    public class Request<TRequest, TResponse> : IRequest
        where TRequest : RequestPackage
        where TResponse : ResponsePackage
    {


        public event EventHandler<ResponseReceivedEventArgs<TResponse>> ResponseReceived;
        protected void RaiseResponseReceived(TResponse response)
        {
            var myevent = ResponseReceived;
            if (myevent != null)
                myevent(this, new ResponseReceivedEventArgs<TResponse>(response));
        }

        public Type RequestType { get { return typeof(TRequest); } }
        public Type ResponseType { get { return typeof(TResponse); } }

        public TRequest RequestPackage { get; protected set; }
        public TResponse ResponsePackage { get; protected set; }

        public IClient Client { get; protected set; }

        public bool isSend { get; protected set; }
        public bool isCompleted { get; protected set; }

        private ManualResetEvent reset = new ManualResetEvent(false);

        public Request(TRequest request, IClient client)
        {
            RequestPackage = request;
            Client = client;
            isSend = false;
            isCompleted = false;
        }

        public void Send()
        {
            Client.SendPackage(RequestPackage);
            isSend = true;
        }

        public void ReceiveResponse(ResponsePackage response)
        {
            if (!(response is TResponse)) throw new ArgumentException("Response has invalid type!", "response");
            ResponsePackage = (TResponse)response;
            isCompleted = true;
            reset.Set();
            RaiseResponseReceived(ResponsePackage);
        }

        public void WaitOne()
        {
            reset.WaitOne();
        }

    }
}

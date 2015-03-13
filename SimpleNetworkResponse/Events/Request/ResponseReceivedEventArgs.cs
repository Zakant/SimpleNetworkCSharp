using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Events.Request
{
    public class ResponseReceivedEventArgs<TResponse> : SimpleNetworkEventArgs
    {
        public TResponse Response { get; protected set; }

        public ResponseReceivedEventArgs(TResponse response)
        {
            Response = response;
        }
    }
}

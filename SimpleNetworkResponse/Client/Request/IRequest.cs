using SimpleNetwork.Package.Packages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client.Request
{
    public interface IRequest
    {
        Type RequestType { get; }
        Type ResponseType { get; }

        IClient Client { get; }

        bool isSend { get; }
        bool isCompleted { get; }

        void WaitOne();

        void ReceiveResponse(ResponsePackage response);
    }
}

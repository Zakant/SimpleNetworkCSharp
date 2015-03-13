using SimpleNetwork.Package.Packages.Request;
using SimpleNetwork.Package.Packages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNetwork.Client.Request
{
    public static class RequestTaskExtensions
    {
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

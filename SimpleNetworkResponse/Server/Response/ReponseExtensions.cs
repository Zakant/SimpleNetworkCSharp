using SimpleNetwork.Package.Packages.Request;
using SimpleNetwork.Package.Packages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Server.Response
{
    public static class ReponseExtensions
    {

        public static void SendResponse(this RequestPackage request, ResponsePackage response)
        {
            response.ResponseId = request.ID;
            request.Client.SendPackage(response);
        }

    }
}

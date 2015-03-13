using SimpleNetwork.Package.Packages;
using SimpleNetwork.Package.Packages.Request;
using SimpleNetwork.Package.Packages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages
{
    public static class PackageExtensions
    {
        // Funktioniert nicht wie erwartet!
        /*
        public static ResponseWrapper toResponse(this IPackage package)
        {
            return new ResponseWrapper(package);
        }

        public static RequestWrapper toRequest(this IPackage package)
        {
            return new RequestWrapper(package);
        }
         */
    }
}

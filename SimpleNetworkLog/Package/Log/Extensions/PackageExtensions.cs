using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    public static class PackageExtensions
    {
        public static bool shouldLogged(this IPackage package)
        {
            return !Attribute.IsDefined(package.GetType(), typeof(NotLoggableAttribute));
        }

    }
}

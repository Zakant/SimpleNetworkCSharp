using SimpleNetwork.Package.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Client
{
    public interface IBaseClient : IPackageProvider, IDisposable
    {
    }
}

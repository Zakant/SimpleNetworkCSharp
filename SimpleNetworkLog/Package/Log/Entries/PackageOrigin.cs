using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries
{
    [Serializable]
    public enum PackageOrigin : int
    {
        Local,
        Remote
    }
}

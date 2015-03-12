using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log.Entries
{
    /// <summary>
    /// Gibt an woher ein Paket stammt.
    /// </summary>
    [Serializable]
    public enum PackageOrigin : int
    {
        /// <summary>
        /// Paket stammt vom Localen Host.
        /// </summary>
        Local,
        /// <summary>
        /// Paket stammt vom Remote Host.
        /// </summary>
        Remote
    }
}

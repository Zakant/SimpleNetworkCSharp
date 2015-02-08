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
        /// Gibt an, dass das Paket vom localen Host stammt.
        /// </summary>
        Local,
        /// <summary>
        /// Gibt an, dass das Paket vom remote Host stammt.
        /// </summary>
        Remote
    }
}

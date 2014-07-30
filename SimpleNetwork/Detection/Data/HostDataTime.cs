using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Detection.Data
{
    /// <summary>
    /// Stellt ein HostData-Objekt mit einem assozierten TimeStamp da.
    /// </summary>
    [Serializable]
    public class HostDataTime
    {
        /// <summary>
        /// Der TimeStamp.
        /// </summary>
        public DateTime stamp;

        /// <summary>
        /// Das HostData-Objekt.
        /// </summary>
        public HostData data;
    }
}

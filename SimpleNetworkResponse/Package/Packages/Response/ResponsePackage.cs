using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Response
{
    /// <summary>
    /// Stellt ein grundlegendes Antwort Paket berei, von dem alle weiteren Antwort Pakete abgeleitet sein müssen.
    /// </summary>
    [Serializable]
    public abstract class ResponsePackage : IPackage
    {
        /// <summary>
        /// Die eindeutige ID der Anfrage zu dieser Antwort.
        /// </summary>
        public ulong ResponseId { get; set; }

    }
}

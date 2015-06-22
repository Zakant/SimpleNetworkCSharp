using SimpleNetwork.Client;
using SimpleNetwork.Package.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Request
{
    /// <summary>
    /// Stellt ein grundlegendes Anfrage Paket da, von dem alle weiteren Anfrage Pakete abgeleitet sein müssen.
    /// </summary>
    [Serializable]
    public abstract class RequestPackage : IPackage
    {
        /// <summary>
        /// Die eindeutige ID der Anfrage.
        /// </summary>
        [InsertId]
        public ulong ID { get; set; }

        /// <summary>
        /// Der Client, der die Anfrage empfangen hat.
        /// </summary>
        [InsertClient]
        public IClient Client { get; set; }
    }
}

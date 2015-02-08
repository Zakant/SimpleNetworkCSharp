using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Handshake
{
    /// <summary>
    /// Stellt ein Paket des Client-Server Handschlages da.
    /// </summary>
    [Serializable]
    public abstract class HandshakePackage : IPackage
    {
        /// <summary>
        /// Die Schluessel-Wert Auflistung da, die als Teil des Handschlages uebertragen wird.
        /// </summary>
        public Dictionary<string, string> KeyValues { get; protected set; }

        /// <summary>
        /// Erzeugt ein neues Paket fuer den Client-Server Handschlag.
        /// </summary>
        public HandshakePackage()
        {
            KeyValues = new Dictionary<string, string>();
        }
    }
}

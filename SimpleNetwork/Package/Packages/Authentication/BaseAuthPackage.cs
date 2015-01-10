using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Authentication
{
    /// <summary>
    /// Stellt ein Paket mit Basis Authentifizierungsdaten da.
    /// </summary>
    public abstract class BaseAuthPackage : IPackage
    {
        /// <summary>
        /// Stellt eine Schlüssel-Wert da, die die Authentifizierungsdaten enthält.
        /// </summary>
        public Dictionary<string, string> Headers { get; protected set; }

        /// <summary>
        /// Erstellt ein neues Paket mit Basis Authentifizierungsdaten.
        /// </summary>
        public BaseAuthPackage()
        {
            Headers = new Dictionary<string, string>();
        }
    }
}

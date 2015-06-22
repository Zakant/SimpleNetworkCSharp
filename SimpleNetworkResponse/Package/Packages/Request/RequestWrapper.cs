using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Request
{
    /// <summary>
    /// Kapselt ein normales Paket in einem Anfrage Paket.
    /// </summary>
    [Serializable]
    public class RequestWrapper : RequestPackage
    {
        /// <summary>
        /// Das ursprüngliche Paket.
        /// </summary>
        public IPackage Package { get; set; }

        /// <summary>
        /// Erstellt ein neues Paket, das ein normales Paket in einem Anfrage Paket kapselt.
        /// </summary>
        /// <param name="package">Das ursprüngliche Paket.</param>
        public RequestWrapper(IPackage package)
        {
            Package = package;
        }
    }
}

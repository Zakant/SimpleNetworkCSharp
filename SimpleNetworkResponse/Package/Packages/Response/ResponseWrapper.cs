using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Response
{
    /// <summary>
    /// Kapselt ein normales Paket in einem Antwort Paket.
    /// </summary>
    [Serializable]
    public class ResponseWrapper : ResponsePackage
    {
        /// <summary>
        /// Das ursprüngliche Paket.
        /// </summary>
        public IPackage Package { get; protected set; }

        /// <summary>
        /// Erstellt ein neues Paket, das ein normales Paket in einem Antwort Paket kapselt.
        /// </summary>
        /// <param name="package">Das ursprüngliche Paket.</param>
        public ResponseWrapper(IPackage package)
        {
            Package = package;
        }

    }
}

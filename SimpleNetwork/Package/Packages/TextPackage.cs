using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages
{
    /// <summary>
    /// Stellt ein Packet da, um Text zu übertragen.
    /// </summary>
    [Serializable]
    public class TextPackage : IPackage
    {
        /// <summary>
        /// Der Text.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Initialisert eine leere Instanz der TextPackage-Klasse.
        /// </summary>
        public TextPackage() { }

        /// <summary>
        /// Initialisiert eine neue Instanz der TextPackage-Klasse unter verwendung des angegeben strings.
        /// </summary>
        /// <param name="value">Der Text.</param>
        public TextPackage(string value)
        {
            Value = value;
        }
    }
}

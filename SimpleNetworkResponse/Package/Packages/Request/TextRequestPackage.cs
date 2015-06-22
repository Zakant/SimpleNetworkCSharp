using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Request
{
    /// <summary>
    /// Stellt ein einfaches Text-Anfrage Paket da.
    /// </summary>
    [Serializable]
    public class TextRequestPackage : RequestPackage
    {
        /// <summary>
        /// Der Text der Anfrage.
        /// </summary>
        public string Text { get; protected set; }

        /// <summary>
        /// Erstellt ein neues Text-Anfrage Paket.
        /// </summary>
        /// <param name="text">Der Text der Anfrage.</param>
        public TextRequestPackage(string text)
        {
            Text = text;
        }
    }
}

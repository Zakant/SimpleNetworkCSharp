using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Response
{
    /// <summary>
    /// Stellt ein einfaches Text-Antwort Paket da.
    /// </summary>
    [Serializable]
    public class TextResponsePackage : ResponsePackage
    {
        /// <summary>
        /// Der Text der Antwort.
        /// </summary>
        public string Text { get; protected set; }

        /// <summary>
        /// Erstellt ein neues Text-Antwort Paket.
        /// </summary>
        /// <param name="text">Der Text der Antwort.</param>
        public TextResponsePackage(string text)
        {
            Text = text;
        }
    }
}

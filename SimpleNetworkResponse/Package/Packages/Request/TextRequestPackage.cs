using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Request
{
    [Serializable]
    public class TextRequestPackage : RequestPackage
    {
        public string Text { get; protected set; }

        public TextRequestPackage(string text)
        {
            Text = text;
        }
    }
}

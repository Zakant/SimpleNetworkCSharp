using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Packages.Response
{
    [Serializable]
 public    class TextResponsePackage : ResponsePackage
    {
             public string Text { get; protected set; }

             public TextResponsePackage(string text)
        {
            Text = text;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    /// <summary>
    /// Gibt an ob ein Paket vom Log ausgeschlossen werden soll. Diese Klasse kann nicht vererbt werden.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Assembly, Inherited = true, AllowMultiple = false)]
    public sealed class NonLoggedAttribute : Attribute
    {
    }
}

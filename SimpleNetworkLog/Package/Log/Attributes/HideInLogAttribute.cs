using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    /// <summary>
    /// Gibt an ob eine Eigenschaft oder ein Feld im Log ausgeblendet werden soll. Diese Klasse kann nicht vererbt werden.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class HideInLogAttribute : Attribute
    {
    }
}

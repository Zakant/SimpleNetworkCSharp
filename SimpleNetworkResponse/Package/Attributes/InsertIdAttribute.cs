using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimpleNetwork.Package.Attributes
{
    /// <summary>
    /// Attribute das angibt, dass an den markierten Stellen einen eindeutige Nummer eingefügt werden soll.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class InsertIdAttribute : Attribute
    {
    }
}

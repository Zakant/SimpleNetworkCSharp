using SimpleNetwork.Package.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimpleNetwork.Package.Attributes
{
    /// <summary>
    /// Attribute das angibt, dass an der markierten Stellen der Client eingefügt werden soll, der das Paket empfangen hat.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class InsertClientAttribute : Attribute
    {
    }
}

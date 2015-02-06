using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleNetwork.Package.Log
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Assembly, Inherited = true, AllowMultiple = false)]
    public sealed class NonLoggedAttribute : Attribute
    {
    }
}

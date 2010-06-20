using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mveEngine
{
    [global::System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class HiddenAttribute : Attribute
    {
    }
}

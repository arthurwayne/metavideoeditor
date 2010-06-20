using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mveEngine
{
    [Flags]
    public enum LogSeverity {
        None        = 0,
        Verbose     = 1,
        Info        = 2, 
        Warning     = 4, 
        Error       = 8
    }
}

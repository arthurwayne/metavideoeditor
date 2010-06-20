using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace mveEngine
{
    public class TraceLogger : LoggerBase {

        public override void LogMessage(LogRow row) {
            Trace.WriteLine(row.ToString());
        }
    }
}

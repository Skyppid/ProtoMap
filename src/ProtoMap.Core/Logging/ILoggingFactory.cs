using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace ProtoMap.Core.Logging
{

    public interface ILoggingFactory
    {
        LoggerConfiguration CreateUsingDefaults(string fileName);

        LoggerConfiguration CreateCustom();
    }
}

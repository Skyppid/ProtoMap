using System;
using System.Globalization;
using System.IO;
using System.Text;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace ProtoMap.Core.Logging.Internal
{
    internal sealed class LoggingFactory : ILoggingFactory
    {
        private readonly string _logDirectory;

        internal LoggingFactory(string? logDirectory = null)
        {
            // Use default if not set
            logDirectory ??= Path.Combine(Environment.CurrentDirectory, "logs");
            _logDirectory = logDirectory;
            EnsureDirectory(logDirectory);
        }

        private void EnsureDirectory(string logDirectory)
        {
            if (Directory.Exists(logDirectory)) return;
            
            try
            {
                Directory.CreateDirectory(logDirectory);
            }
            catch (UnauthorizedAccessException uae)
            {
                throw new Exception("Could not setup logging directory. Missing access rights.", uae);
            }
        }

        public LoggerConfiguration CreateUsingDefaults(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Value cannot be null or empty.", nameof(fileName));

            LoggerConfiguration config = new LoggerConfiguration();
            config.WriteTo.Async(c => c.File(new JsonFormatter(null, true, CultureInfo.GetCultureInfo("en-US")),
                Path.Combine(_logDirectory, fileName), LogEventLevel.Information, null, null, true, false, null,
                RollingInterval.Infinite,
                true, 7, Encoding.UTF8, null));
            return config;
        }

        public LoggerConfiguration CreateCustom()
        {
            return new LoggerConfiguration();
        }
    }
}

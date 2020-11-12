using Serilog;

namespace ProtoMap.Core.Logging
{
    /// <summary>
    /// Interfaces which exposes default configurations for loggers.
    /// </summary>
    public interface ILoggingFactory
    {
        /// <summary>
        /// Creates a default configuration which enforces the default path (so only the file name but not the path should be provided).
        /// </summary>
        /// <param name="fileName">The file name <c>only</c> (no full path).</param>
        /// <returns>The pre-configured logger configuration.</returns>
        LoggerConfiguration CreateUsingDefaults(string fileName);

        /// <summary>
        /// Creates a configuration with default settings but does not enforce the log path.
        /// </summary>
        /// <param name="fileName">The full path to the log file.</param>
        /// <returns>The pre-configured logger configuration.</returns>
        LoggerConfiguration CreateUsingDefaultsCustomPath(string fileName);

        /// <summary>
        /// Creates a custom configuration without any defaults. This is equal to <code>LoggerConfiguration config = new LoggerConfiguration()</code> from Serilog itself.
        /// </summary>
        /// <returns>A blank configuration.</returns>
        LoggerConfiguration CreateCustom();
    }
}
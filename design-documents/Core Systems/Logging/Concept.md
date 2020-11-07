# Logging Concept

For logging ProtoMap will utilize **Serilog**.



## General Availability

Logging is available everywhere. In case logs should be written to the main application log, a reference to the main `ILogger` can be injected. In case a separate log should be used, it's recommended to inject the `ILoggingFactory` which exposes methods for logger creation with defaults set (recommended) or simply bare logs (which can also be achieved through `new LoggerConfiguration()` using Serilog itself).



## Main Application Log

The main application log only exposes **Information** or higher levels. The main logger does support a `LoggingSwitch` which can alter this. This switch will be exposed in the application settings, once implemented at a later time.
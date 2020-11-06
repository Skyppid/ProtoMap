using DryIoc;
using ProtoMap.Core.Logging;
using ProtoMap.Core.Logging.Internal;

namespace ProtoMap.Core
{
    internal static class CoreInitialization
    {
        internal static void InitializeCoreServices(IContainer container)
        {
            container.Register<ILoggingFactory, LoggingFactory>(Reuse.Singleton,
                Made.Of(() => new LoggingFactory(null)));
        }
    }
}
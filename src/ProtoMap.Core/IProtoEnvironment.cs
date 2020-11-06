using DryIoc;

namespace ProtoMap.Core
{
    /// <summary>
    /// The ProtoMap environement is the most basic of all interfaces and shared application-wide. It's implemented as singleton and provides access
    /// to the IOC container and other globally available components.
    /// </summary>
    public interface IProtoEnvironment
    {
        /// <summary>
        /// Gets the application-wide IOC container.
        /// </summary>
        IContainer Container { get; }
    }
}
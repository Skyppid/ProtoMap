using System;
using System.IO;
using DryIoc;
using ProtoMap.Core.Logging;
using Serilog;

namespace ProtoMap.Core.ProjectSystem
{
    /// <summary>
    /// Base class which exposes properties and methods for interaction with and manipulation of projects.
    /// </summary>
    public abstract class ProtoProjectBase
    {
        /// <summary>
        /// Constructor base for a project.
        /// </summary>
        /// <param name="environment">The ProtoMap environment.</param>
        /// <param name="factory">The logging factory.</param>
        /// <param name="projectDirectory">The project directory.</param>
        protected ProtoProjectBase(IProtoEnvironment environment, ILoggingFactory factory, DirectoryInfo projectDirectory, ProjectMetadataBase? meta = null)
        {
            Environment = environment;
            Meta = environment.Container.Resolve<ProjectMetadataBase>();
            ProjectDirectory = projectDirectory;
            ProjectLogging = factory.CreateUsingDefaultsCustomPath(Path.Combine(projectDirectory.FullName, "project.log"))
                .CreateLogger();

            if (meta == null) return;
            try
            {
                meta.CopyTo(Meta);
            }
            catch (NotSupportedException)
            {
                ProjectLogging.Warning("The provided meta data cannot be used. It does not support copying.");
            }
        }

        /// <summary>
        /// Gets the meta data for this project.
        /// </summary>
        public ProjectMetadataBase Meta { get; }

        /// <summary>
        /// Gets the project directory.
        /// </summary>
        public DirectoryInfo ProjectDirectory { get; }

        /// <summary>
        /// Gets the environment for this project.
        /// </summary>
        public IProtoEnvironment Environment { get; }

        /// <summary>
        /// Gets the project logger.
        /// </summary>
        protected ILogger ProjectLogging { get; private set; }
    }
}

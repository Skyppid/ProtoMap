using System.Threading.Tasks;

namespace ProtoMap.Core.ProjectSystem
{
    /// <summary>
    /// Exposes methods for creating projects of certain types.
    /// </summary>
    public abstract class ProjectFactoryBase<TProject>
    {
        /// <summary>
        /// Creates a new project and sets up the project directory as required.
        /// </summary>
        /// <returns>The created project instance.</returns>
        public abstract Task<TProject> CreateProject(ProjectMetadataBase meta);

        /// <summary>
        /// Loads a project from a file / folder.
        /// </summary>
        /// <param name="projectFileOrDirectory">The project file or a project directory (auto discovery).</param>
        /// <returns></returns>
        public abstract Task<TProject> LoadProject(string projectFileOrDirectory);
    }
}

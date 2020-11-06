namespace ProtoMap.Models.ProjectSystem
{
    public readonly struct RecentProjectInfoModel
    {
        public string Name { get; }

        public string ProjectFile { get; }

        public RecentProjectInfoModel(string name, string projectFile)
        {
            Name = name;
            ProjectFile = projectFile;
        }
    }
}
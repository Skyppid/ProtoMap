using System.Collections.ObjectModel;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ProtoMap.Models.ProjectSystem;

namespace ProtoMap.ViewModels.Launcher
{
    public class LauncherPrimaryViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _manager;

        public LauncherPrimaryViewModel(IRegionManager manager)
        {
            _manager = manager;
            RecentProjects = new ObservableCollection<RecentProjectInfoModel>();

            CreateProjectCommand = new DelegateCommand(ExecuteCreateProject);
            LoadProjectCommand = new DelegateCommand(ExecuteLoadProject);

#if DEBUG
            PopulateTestItems();
#endif
        }

        public DelegateCommand CreateProjectCommand { get; }

        public DelegateCommand LoadProjectCommand { get; }

        public ObservableCollection<RecentProjectInfoModel> RecentProjects { get; }

        private void ExecuteCreateProject()
        {
            _manager.RequestNavigate("ContentRegion", "CreateProjectView");
        }

        private void ExecuteLoadProject()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Project";
            dialog.Filter = "ProtoMap Project|*.pmap";

            if (dialog.ShowDialog() == false) return;

            NavigationParameters p = new NavigationParameters();
            p.Add("project", dialog.SafeFileName);
            _manager.RequestNavigate("ContentRegion", "BootstrapView", null, p);
        }

        private void PopulateTestItems()
        {
            for (int i = 0; i < 5; i++)
                RecentProjects.Add(new RecentProjectInfoModel($"Test Project {i}", $"C:\\Test\\Project_{i}.pmap"));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
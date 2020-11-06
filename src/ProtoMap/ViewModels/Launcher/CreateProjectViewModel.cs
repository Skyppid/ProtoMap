using Prism.Mvvm;
using Prism.Regions;

namespace ProtoMap.ViewModels.Launcher
{
    public class CreateProjectViewModel : BindableBase, INavigationAware
    {
        public CreateProjectViewModel()
        {
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
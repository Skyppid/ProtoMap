using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Prism.Mvvm;
using Prism.Regions;
using ProtoMap.Core.Logging;

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

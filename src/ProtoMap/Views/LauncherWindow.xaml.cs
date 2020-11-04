using Prism.Regions;
using ProtoMap.Views.Launcher;

namespace ProtoMap.Views
{
    /// <summary>
    /// Interaction logic for LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow
    {
        public LauncherWindow(IRegionManager manager)
        {
            InitializeComponent();

            manager.RegisterViewWithRegion("ContentRegion", typeof(LauncherPrimaryView));
            manager.RegisterViewWithRegion("ContentRegion", typeof(CreateProjectView));
        }
    }
}

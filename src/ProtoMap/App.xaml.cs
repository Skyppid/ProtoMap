using System.Windows;
using Prism.DryIoc;
using Prism.Ioc;
using ProtoMap.Views;
using ProtoMap.Views.Launcher;

namespace ProtoMap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LauncherPrimaryView>();
            containerRegistry.RegisterForNavigation<CreateProjectView>();
        }
        
        protected override Window CreateShell()
        {
            var window = Container.Resolve<LauncherWindow>();
            window.Visibility = Visibility.Collapsed;
            return window;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}

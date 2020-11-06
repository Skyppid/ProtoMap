using System;
using System.Diagnostics;
using System.Windows;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using ProtoMap.Core;
using ProtoMap.Views;
using ProtoMap.Views.Launcher;

namespace ProtoMap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication, IProtoEnvironment
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var container = containerRegistry.GetContainer();

            // Setup the core internal services
            CoreInitialization.InitializeCoreServices(container);

            // Register this application as the IProtoEnvironment instance application-wide
            container.RegisterInstance<IProtoEnvironment>(this, IfAlreadyRegistered.Throw);

            // Setup views
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

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is ContainerException containerException)
                if (Debugger.IsAttached)
                    Debugger.Break();

            // TODO: Implement fatal logging
        }
    }
}
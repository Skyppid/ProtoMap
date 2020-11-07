using System;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using ProtoMap.Core;
using ProtoMap.Core.Logging;
using ProtoMap.Views;
using ProtoMap.Views.Launcher;
using Serilog;

namespace ProtoMap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication, IProtoEnvironment
    {
        private ILogger _mainLogger;

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            
            // Suppress warning as setting the container is one of the first actions and from then on it won't be null
            // Same for the app logger instance
            Container = null!;
            _mainLogger = null!;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public new Container Container { get; private set; }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var container = Container = (Container)containerRegistry.GetContainer();
            
            // Setup the core internal services
            CoreInitialization.InitializeCoreServices(container);

            // Setup application logger
            ILoggingFactory factory = container.Resolve<ILoggingFactory>();
            _mainLogger = factory.CreateUsingDefaults("app.log").CreateLogger();
            container.RegisterInstance(_mainLogger, IfAlreadyRegistered.Keep);

            _mainLogger.Debug("Application service registration ...");

            // Register this application as the IProtoEnvironment instance application-wide
            container.RegisterInstance<IProtoEnvironment>(this, IfAlreadyRegistered.Throw);

            _mainLogger.Debug("Registration of views ...");
            // Setup views
            containerRegistry.RegisterForNavigation<LauncherPrimaryView>();
            containerRegistry.RegisterForNavigation<CreateProjectView>();
        }

        protected override Window CreateShell()
        {
            _mainLogger.Debug("Shell is being initialized ...");
            var window = Container.Resolve<LauncherWindow>();
            window.Visibility = Visibility.Collapsed;
            return window;
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception generalException)
                _mainLogger.Fatal(generalException, "Fatal unhandled exception occured during runtime.");
            else _mainLogger.Fatal("Fatal unhandled exception occured. No information is present.");
        }

        private void TaskSchedulerOnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            _mainLogger.Fatal("Unhandled exception occured inside a task: {@Exception}.", e.Exception);
            e.SetObserved();
        }
    }
}
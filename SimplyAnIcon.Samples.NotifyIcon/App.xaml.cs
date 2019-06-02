using System.Windows;
using Com.Ericmas001.DependencyInjection.RegistrantFinders;
using Com.Ericmas001.DependencyInjection.Unity;
using Hardcodet.Wpf.TaskbarNotification;
using SimplyAnIcon.Core.ViewModels.Interfaces;
using Unity;

namespace SimplyAnIcon.Samples.NotifyIcon
{
    public partial class App
    {
        private TaskbarIcon _notifyIcon;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new UnityContainer();

            new RegistrantFinderBuilder()
                .AddAssemblyPrefix("SimplyAnIcon")
                .Build()
                .GetAllRegistrations()
                .RegisterTypes(container);

            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            if (_notifyIcon != null)
            {
                var vm = container.Resolve<ISimplyAnIconViewModel>();
                await vm.LoadIcon();
                _notifyIcon.DataContext = vm;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}

using System.Windows;
using Com.Ericmas001.DependencyInjection.RegistrantFinders;
using Com.Ericmas001.DependencyInjection.Unity;
using Hardcodet.Wpf.TaskbarNotification;
using SimplyAnIcon.Common.ViewModels.Interfaces;
using Unity;

namespace SimplyAnIcon.Samples.NotifyIcon
{
    public partial class App
    {
        private TaskbarIcon _notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new UnityContainer();
            
            new RegistrantFinderBuilder()
                .AddAssemblyPrefix("SimplyAnIcon.Common")
                .AddAssemblyPrefix("SimplyAnIcon.Samples")
                .Build()
                .GetAllRegistrations()
                .RegisterTypes(container);
            
            _notifyIcon = (TaskbarIcon) FindResource("NotifyIcon");
            if(_notifyIcon != null)
                _notifyIcon.DataContext = container.Resolve<ISimplyAnIconViewModel>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}

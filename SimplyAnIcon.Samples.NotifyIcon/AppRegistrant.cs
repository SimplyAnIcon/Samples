using Com.Ericmas001.DependencyInjection.Registrants;
using SimplyAnIcon.Core.ViewModels.Interfaces;
using SimplyAnIcon.Samples.NotifyIcon.Services;
using SimplyAnIcon.Samples.NotifyIcon.Services.Interfaces;
using SimplyAnIcon.Samples.NotifyIcon.ViewModels;

namespace SimplyAnIcon.Samples.NotifyIcon
{
    public class AppRegistrant : AbstractRegistrant
    {
        protected override void RegisterEverything()
        {
            RegisterViewModels();
            RegisterServices();
        }

        private void RegisterViewModels()
        {
            Register<ISimplyAnIconViewModel, NotifyIconViewModel>();
        }
        private void RegisterServices()
        {
            Register<IIconLogicService, IconLogicService>();
        }
    }
}

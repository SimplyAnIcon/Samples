using Com.Ericmas001.DependencyInjection.Registrants;
using SimplyAnIcon.Core.Helpers.Interfaces;
using SimplyAnIcon.Core.ViewModels.Interfaces;
using SimplyAnIcon.Samples.NotifyIcon.Helpers;
using SimplyAnIcon.Samples.NotifyIcon.ViewModels;

namespace SimplyAnIcon.Samples.NotifyIcon
{
    public class AppRegistrant : AbstractRegistrant
    {
        protected override void RegisterEverything()
        {
            RegisterHelpers();
            RegisterViewModels();
        }

        private void RegisterHelpers()
        {
            Register<IInstanceResolverHelper, UnityInstanceResolverHelper>();
        }

        private void RegisterViewModels()
        {
            Register<ISimplyAnIconViewModel, NotifyIconViewModel>();
            Register<IConfigViewModel, ConfigViewModel>();
        }
    }
}

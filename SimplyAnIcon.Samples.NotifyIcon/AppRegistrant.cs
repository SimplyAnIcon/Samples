using Com.Ericmas001.DependencyInjection.Registrants;
using SimplyAnIcon.Samples.NotifyIcon.Services;
using SimplyAnIcon.Samples.NotifyIcon.Services.Interfaces;
using SimplyAnIcon.Samples.NotifyIcon.ViewModels;
using SimplyAnIcon.Common.ViewModels.Interfaces;
using SimplyAnIcon.Samples.NotifyIcon.Helpers;
using SimplyAnIcon.Samples.NotifyIcon.Helpers.Interfaces;
using SimplyAnIcon.Samples.NotifyIcon.Settings;
using SimplyAnIcon.Samples.NotifyIcon.Settings.Interface;

namespace SimplyAnIcon.Samples.NotifyIcon
{
    public class AppRegistrant : AbstractRegistrant
    {
        protected override void RegisterEverything()
        {
            RegisterViewModels();
            RegisterServices();
            RegisterHelpers();
            RegisterSettings();
        }

        private void RegisterHelpers()
        {
            Register<IWindowsHelper, WindowsHelper>();
            Register<IJsonHelper, JsonHelper>();
        }

        private void RegisterViewModels()
        {
            Register<ISimplyAnIconViewModel, NotifyIconViewModel>();
        }
        private void RegisterServices()
        {
            Register<IIconLogicService, IconLogicService>();
        }
        private void RegisterSettings()
        {
            Register<IPluginSettings, PluginSettings>();
        }
    }
}

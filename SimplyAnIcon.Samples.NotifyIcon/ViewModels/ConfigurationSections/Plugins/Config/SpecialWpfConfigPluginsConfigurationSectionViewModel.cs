using SimplyAnIcon.Plugins.Wpf.V1;
using SimplyAnIcon.Samples.NotifyIcon.Settings.Interface;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins.Config
{
    public class SpecialWpfConfigPluginsConfigurationSectionViewModel : AbstractConfigPluginsConfigurationSectionViewModel
    {
        public SpecialWpfConfigPluginsConfigurationSectionViewModel(IPluginSettings pluginSettings) : base(pluginSettings)
        {
        }

        public void OnInit(ISimplyAWpfPlugin plugin)
        {
            OnInternalInit(plugin);
        }
    }
}

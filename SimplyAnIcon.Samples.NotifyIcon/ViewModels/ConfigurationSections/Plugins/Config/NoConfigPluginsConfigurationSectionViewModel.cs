using SimplyAnIcon.Plugins.V1;
using SimplyAnIcon.Samples.NotifyIcon.Settings.Interface;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins.Config
{
    public class NoConfigPluginsConfigurationSectionViewModel : AbstractConfigPluginsConfigurationSectionViewModel
    {
        public NoConfigPluginsConfigurationSectionViewModel(IPluginSettings pluginSettings) : base(pluginSettings)
        {
        }

        public void OnInit(ISimplyAPlugin plugin)
        {
            OnInternalInit(plugin);
        }
    }
}

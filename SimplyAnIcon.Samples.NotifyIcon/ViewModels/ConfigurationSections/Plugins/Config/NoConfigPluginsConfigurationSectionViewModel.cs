using SimplyAnIcon.Plugins.V1;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins.Config
{
    public class NoConfigPluginsConfigurationSectionViewModel : AbstractConfigPluginsConfigurationSectionViewModel
    {
        public void OnInit(ISimplyAPlugin plugin)
        {
            OnInternalInit(plugin);
        }
    }
}

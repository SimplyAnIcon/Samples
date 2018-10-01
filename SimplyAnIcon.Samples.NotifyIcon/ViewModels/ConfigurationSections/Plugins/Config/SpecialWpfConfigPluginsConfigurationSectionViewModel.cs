using SimplyAnIcon.Plugins.Wpf.V1;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins.Config
{
    public class SpecialWpfConfigPluginsConfigurationSectionViewModel : AbstractConfigPluginsConfigurationSectionViewModel
    {
        public void OnInit(ISimplyAWpfPlugin plugin)
        {
            OnInternalInit(plugin);
        }
    }
}

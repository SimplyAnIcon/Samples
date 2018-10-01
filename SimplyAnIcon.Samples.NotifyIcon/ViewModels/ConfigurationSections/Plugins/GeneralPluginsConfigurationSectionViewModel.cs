using System.Collections.Generic;
using SimplyAnIcon.Common.Models;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins
{
    public class GeneralPluginsConfigurationSectionViewModel : IConfigurationSectionViewModel
    {
        public string Name => "General";
        public object IconPath => null;

        public IEnumerable<IConfigurationSectionViewModel> ChildrenSections => new IConfigurationSectionViewModel[0];
        public void OnInit(PluginCatalog catalog)
        {
            throw new System.NotImplementedException();
        }
    }
}

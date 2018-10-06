using System.Collections.Generic;
using SimplyAnIcon.Common.ViewModels.Interfaces;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections
{
    public class GeneralConfigurationSectionViewModel : IConfigurationSectionViewModel
    {
        public string Name => "General";
        public object IconPath => null;

        public IEnumerable<IConfigurationSectionViewModel> ChildrenSections => new IConfigurationSectionViewModel[0];
    }
}

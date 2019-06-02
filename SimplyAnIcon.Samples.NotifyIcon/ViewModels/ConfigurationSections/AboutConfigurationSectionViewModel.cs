using System.Collections.Generic;
using SimplyAnIcon.Core.ViewModels.Interfaces;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections
{
    public class AboutConfigurationSectionViewModel : IConfigurationSectionViewModel
    {
        public string Name => "About";
        public object IconPath => null;

        public IEnumerable<IConfigurationSectionViewModel> ChildrenSections => new IConfigurationSectionViewModel[0];
    }
}

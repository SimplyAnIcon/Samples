using System.Collections.Generic;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels
{
    public interface IConfigurationSectionViewModel
    {
        string Name { get; }
        object IconPath { get; }
        IEnumerable<IConfigurationSectionViewModel> ChildrenSections { get; }
    }
}

using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using SimplyAnIcon.Common.Models;
using SimplyAnIcon.Plugins.Wpf.Util;
using SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections
{
    public class PluginsConfigurationSectionViewModel : IConfigurationSectionViewModel
    {
        private readonly IResolverService _resolverService;
        private readonly FastObservableCollection<IConfigurationSectionViewModel> _sections = new FastObservableCollection<IConfigurationSectionViewModel>();

        public string Name => "Plugins";
        public object IconPath => null;

        public IEnumerable<IConfigurationSectionViewModel> ChildrenSections => _sections;
        public PluginsConfigurationSectionViewModel(IResolverService resolverService)
        {
            _resolverService = resolverService;
        }

        public void OnInit(PluginCatalog catalog)
        {
            _sections.Add(_resolverService.Resolve<GeneralPluginsConfigurationSectionViewModel>());

            var plugins = _resolverService.Resolve<SpecificPluginsConfigurationSectionViewModel>();
            plugins.OnInit(catalog);
            _sections.Add(plugins);
        }
    }
}

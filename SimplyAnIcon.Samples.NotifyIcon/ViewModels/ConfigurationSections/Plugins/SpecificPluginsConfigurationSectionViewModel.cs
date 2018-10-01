using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using SimplyAnIcon.Common.Models;
using SimplyAnIcon.Plugins.Wpf.Util;
using SimplyAnIcon.Plugins.Wpf.V1;
using SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins.Config;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins
{
    public class SpecificPluginsConfigurationSectionViewModel : IConfigurationSectionViewModel
    {
        private readonly IResolverService _resolverService;
        private readonly FastObservableCollection<IConfigurationSectionViewModel> _sections = new FastObservableCollection<IConfigurationSectionViewModel>();

        public string Name => "Plugins Configuration";
        public object IconPath => null;

        public IEnumerable<IConfigurationSectionViewModel> ChildrenSections => _sections;

        public SpecificPluginsConfigurationSectionViewModel(IResolverService resolverService)
        {
            _resolverService = resolverService;
        }

        public void OnInit(PluginCatalog catalog)
        {
            foreach (var plugin in catalog.PluginInstances.Concat(catalog.PluginWpfInstances).OrderBy(x => x.Name))
            {
                if (plugin is ISimplyAWpfPlugin wpfPlugin && wpfPlugin.CustomConfigControl != null)
                {
                    var section = _resolverService.Resolve<SpecialWpfConfigPluginsConfigurationSectionViewModel>();
                    section.OnInit(wpfPlugin);
                    _sections.Add(section);
                }
                else if (plugin.ConfigurationItems != null && plugin.ConfigurationItems.Any())
                {
                    var section = _resolverService.Resolve<BasicConfigPluginsConfigurationSectionViewModel>();
                    section.OnInit(plugin);
                    _sections.Add(section);
                }
                else
                {
                    var section = _resolverService.Resolve<NoConfigPluginsConfigurationSectionViewModel>();
                    section.OnInit(plugin);
                    _sections.Add(section);
                }
            }
        }
    }
}

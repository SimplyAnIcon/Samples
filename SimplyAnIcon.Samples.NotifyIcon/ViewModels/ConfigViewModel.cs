using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using GalaSoft.MvvmLight;
using SimplyAnIcon.Common.Models;
using SimplyAnIcon.Plugins.Wpf.Util;
using SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels
{
    public class ConfigViewModel : ViewModelBase
    {
        private readonly IResolverService _resolverService;
        private readonly FastObservableCollection<IConfigurationSectionViewModel> _sections = new FastObservableCollection<IConfigurationSectionViewModel>();

        private IConfigurationSectionViewModel _selectedSection;
        public IEnumerable<IConfigurationSectionViewModel> Sections => _sections;

        public IConfigurationSectionViewModel SelectedSection
        {
            get => _selectedSection;
            set => Set(ref _selectedSection, value);
        }

        public ConfigViewModel(IResolverService resolverService)
        {
            _resolverService = resolverService;
        }

        public void OnInit(PluginCatalog catalog)
        {
            _sections.Add(_resolverService.Resolve<GeneralConfigurationSectionViewModel>());

            var plugins = _resolverService.Resolve<PluginsConfigurationSectionViewModel>();
            plugins.OnInit(catalog);
            _sections.Add(plugins);

            _sections.Add(_resolverService.Resolve<AboutConfigurationSectionViewModel>());

            SelectedSection = _sections.First();
        }
    }
}

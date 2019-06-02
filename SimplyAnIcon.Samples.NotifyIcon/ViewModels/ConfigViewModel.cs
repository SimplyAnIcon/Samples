using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using SimplyAnIcon.Core.Models;
using SimplyAnIcon.Core.ViewModels;
using SimplyAnIcon.Core.ViewModels.Interfaces;
using SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels
{
    public class ConfigViewModel : AbstractConfigViewModel
    {
        private readonly IResolverService _resolverService;
        public ConfigViewModel(IResolverService resolverService) : base(resolverService)
        {
            _resolverService = resolverService;
            IconSource = "/cool.ico";
        }

        protected override IEnumerable<IConfigurationSectionViewModel> GenerateSections(IEnumerable<PluginInfo> catalog)
        {
            yield return _resolverService.Resolve<GeneralConfigurationSectionViewModel>();

            foreach (var vm in base.GenerateSections(catalog))
                yield return vm;

            yield return _resolverService.Resolve<AboutConfigurationSectionViewModel>();
        }
    }
}

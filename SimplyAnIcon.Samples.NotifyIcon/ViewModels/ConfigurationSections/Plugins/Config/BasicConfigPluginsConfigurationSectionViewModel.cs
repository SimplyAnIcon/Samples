using System;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using SimplyAnIcon.Plugins.V1;
using SimplyAnIcon.Plugins.V1.Settings;
using SimplyAnIcon.Plugins.Wpf.Util;
using SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationItems;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins.Config
{
    public class BasicConfigPluginsConfigurationSectionViewModel : AbstractConfigPluginsConfigurationSectionViewModel
    {
        private readonly IResolverService _resolverService;

        public FastObservableCollection<AbstractConfigurationItemViewModel> Items { get; } = new FastObservableCollection<AbstractConfigurationItemViewModel>();

        public BasicConfigPluginsConfigurationSectionViewModel(IResolverService resolverService)
        {
            _resolverService = resolverService;
        }

        public void OnInit(ISimplyAPlugin plugin)
        {
            OnInternalInit(plugin);
            foreach (var it in plugin.ConfigurationItems)
            {
                var getFunc = (Func<object>)(() => plugin.GetConfigurationValue(it.Key));
                var setFunc = (Action<object>)(x => plugin.SetConfigurationValue(it.Key, x));
                switch (it)
                {
                    case BoolSettingValue boolIt:
                    {
                        var itVm = _resolverService.Resolve<BoolConfigurationItemViewModel>();
                        itVm.OnInit(boolIt, getFunc, setFunc);
                        Items.Add(itVm);
                        break;
                    }
                    case StringListSettingValue listIt:
                    {
                        var itVm = _resolverService.Resolve<StringListConfigurationItemViewModel>();
                        itVm.OnInit(listIt, getFunc, setFunc);
                        Items.Add(itVm);
                        break;
                    }
                    case StringSettingValue strIt:
                    {
                        var itVm = _resolverService.Resolve<StringConfigurationItemViewModel>();
                        itVm.OnInit(strIt, getFunc, setFunc);
                        Items.Add(itVm);
                        break;
                    }
                    case IntSettingValue intIt:
                    {
                        var itVm = _resolverService.Resolve<IntConfigurationItemViewModel>();
                        itVm.OnInit(intIt, getFunc, setFunc);
                        Items.Add(itVm);
                        break;
                    }
                }
            }
        }
    }
}

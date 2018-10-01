using System;
using System.Linq;
using System.Windows.Input;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using SimplyAnIcon.Plugins.V1;
using SimplyAnIcon.Plugins.V1.Settings;
using SimplyAnIcon.Plugins.Wpf.Util;
using SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationItems;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins.Config
{
    public class BasicConfigPluginsConfigurationSectionViewModel : AbstractConfigPluginsConfigurationSectionViewModel
    {
        private readonly IResolverService _resolverService;
        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand = _saveCommand ?? new RelayCommand(OnSave, CanSave);

        private bool CanSave()
        {
            return Items.All(x => x.IsValid());
        }

        private void OnSave()
        {
            Items.ToList().ForEach(x => Plugin.SetConfigurationValue(x.Setting.Key, x.ResultValue));
        }

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
                var defaultValue = plugin.GetConfigurationValue(it.Key);
                switch (it)
                {
                    case BoolSettingValue boolIt:
                    {
                        var itVm = _resolverService.Resolve<BoolConfigurationItemViewModel>();
                        itVm.OnInit(boolIt, defaultValue);
                        Items.Add(itVm);
                        break;
                    }
                    case StringListSettingValue listIt:
                    {
                        var itVm = _resolverService.Resolve<StringListConfigurationItemViewModel>();
                        itVm.OnInit(listIt, defaultValue);
                        Items.Add(itVm);
                        break;
                    }
                    case StringSettingValue strIt:
                    {
                        var itVm = _resolverService.Resolve<StringConfigurationItemViewModel>();
                        itVm.OnInit(strIt, defaultValue);
                        Items.Add(itVm);
                        break;
                    }
                    case IntSettingValue intIt:
                    {
                        var itVm = _resolverService.Resolve<IntConfigurationItemViewModel>();
                        itVm.OnInit(intIt, defaultValue);
                        Items.Add(itVm);
                        break;
                    }
                }
            }
        }
    }
}

using System.Collections.Generic;
using GalaSoft.MvvmLight;
using SimplyAnIcon.Plugins.V1;
using SimplyAnIcon.Samples.NotifyIcon.Settings.Interface;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins.Config
{
    public abstract class AbstractConfigPluginsConfigurationSectionViewModel : ViewModelBase, IConfigurationSectionViewModel
    {
        private readonly IPluginSettings _pluginSettings;
        private bool _isActivated;

        public ISimplyAPlugin Plugin { get; private set; }
        public string Name => Plugin.Name;
        public virtual object IconPath => null;
        public IEnumerable<IConfigurationSectionViewModel> ChildrenSections => new IConfigurationSectionViewModel[0];

        protected AbstractConfigPluginsConfigurationSectionViewModel(IPluginSettings pluginSettings)
        {
            _pluginSettings = pluginSettings;
        }

        public bool IsActivated
        {
            get => _isActivated;
            set
            {
                Set(ref _isActivated, value);
                _pluginSettings.SetActivationStatus(Name, value);
            }
        }

        protected virtual void OnInternalInit(ISimplyAPlugin plugin)
        {
            Plugin = plugin;
            IsActivated = _pluginSettings.IsActive(Name);
        }
    }
}

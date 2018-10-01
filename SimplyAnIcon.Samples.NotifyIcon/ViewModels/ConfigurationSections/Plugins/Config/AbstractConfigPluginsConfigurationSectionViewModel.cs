using System.Collections.Generic;
using GalaSoft.MvvmLight;
using SimplyAnIcon.Plugins.V1;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationSections.Plugins.Config
{
    public abstract class AbstractConfigPluginsConfigurationSectionViewModel : ViewModelBase, IConfigurationSectionViewModel
    {
        private bool _isActivated;
        public ISimplyAPlugin Plugin { get; private set; }
        public string Name => Plugin.Name;
        public virtual object IconPath => null;
        public IEnumerable<IConfigurationSectionViewModel> ChildrenSections => new IConfigurationSectionViewModel[0];

        public bool IsActivated
        {
            get => _isActivated;
            set => Set(ref _isActivated, value);
        }

        protected virtual void OnInternalInit(ISimplyAPlugin plugin)
        {
            Plugin = plugin;
        }
    }
}

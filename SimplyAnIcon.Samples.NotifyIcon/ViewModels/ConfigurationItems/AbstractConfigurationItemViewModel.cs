using System;
using GalaSoft.MvvmLight;
using SimplyAnIcon.Plugins.V1.Settings;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationItems
{
    public abstract class AbstractConfigurationItemViewModel : ViewModelBase
    {
        public AbstractSettingValue Setting { get; private set; }

        public virtual bool IsValid() => true;
        public abstract object ResultValue { get; }

        public virtual void OnInit(AbstractSettingValue setting, object defaultValue)
        {
            Setting = setting;
        }
    }

    public abstract class AbstractConfigurationItemViewModel<T> : AbstractConfigurationItemViewModel where T : AbstractSettingValue
    {
        public new T Setting { get; private set; }

        public override void OnInit(AbstractSettingValue setting, object defaultValue)
        {
            base.OnInit(setting, defaultValue);
            if (setting is T tSetting)
                Setting = tSetting;
            else
                throw new ArgumentException("Wrong Setting Type. Should be of type " + typeof(T).FullName,
                    nameof(setting));
            OnInit(defaultValue);
        }

        protected abstract void OnInit(object defaultValue);
    }
}

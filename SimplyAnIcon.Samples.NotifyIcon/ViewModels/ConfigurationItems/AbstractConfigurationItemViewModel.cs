using System;
using GalaSoft.MvvmLight;
using SimplyAnIcon.Plugins.V1.Settings;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationItems
{
    public abstract class AbstractConfigurationItemViewModel : ViewModelBase
    {
        public AbstractSettingValue Setting { get; private set; }

        protected Func<object> GetFunc { get; private set; }
        protected Action<object> SetFunc { get; private set; }

        public virtual void OnInit(AbstractSettingValue setting, Func<object> getFunc, Action<object> setFunc)
        {
            Setting = setting;
            GetFunc = getFunc;
            SetFunc = setFunc;
        }
    }

    public abstract class AbstractConfigurationItemViewModel<T> : AbstractConfigurationItemViewModel where T : AbstractSettingValue
    {
        public new T Setting { get; private set; }

        public override void OnInit(AbstractSettingValue setting, Func<object> getFunc, Action<object> setFunc)
        {
            base.OnInit(setting, getFunc, setFunc);
            if (setting is T tSetting)
                Setting = tSetting;
            else
                throw new ArgumentException("Wrong Setting Type. Should be of type " + typeof(T).FullName,
                    nameof(setting));
            OnInit();
        }

        protected abstract void OnInit();
    }
}

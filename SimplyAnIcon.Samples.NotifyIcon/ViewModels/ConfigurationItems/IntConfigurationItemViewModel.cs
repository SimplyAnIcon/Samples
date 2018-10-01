﻿using SimplyAnIcon.Plugins.V1.Settings;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationItems
{
    public class IntConfigurationItemViewModel : AbstractConfigurationItemViewModel<IntSettingValue>
    {
        private int _value;

        public int Value
        {
            get => _value;
            set => Set(ref _value, value);
        }

        protected override void OnInit()
        {
            Value = (int)GetFunc();
        }
    }
}

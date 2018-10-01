using SimplyAnIcon.Plugins.V1.Settings;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationItems
{
    public class BoolConfigurationItemViewModel : AbstractConfigurationItemViewModel<BoolSettingValue>
    {
        private bool _value;

        public bool Value
        {
            get => _value;
            set => Set(ref _value, value);
        }

        public override object ResultValue => Value;

        protected override void OnInit(object defaultValue)
        {
            Value = (bool)defaultValue;
        }
    }
}

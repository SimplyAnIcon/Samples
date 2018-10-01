using SimplyAnIcon.Plugins.V1.Settings;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationItems
{
    public class StringListConfigurationItemViewModel : AbstractConfigurationItemViewModel<StringListSettingValue>
    {
        private string _value;

        public string Value
        {
            get => _value;
            set => Set(ref _value, value);
        }

        protected override void OnInit()
        {
            Value = (string)GetFunc();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using SimplyAnIcon.Plugins.V1.Settings;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationItems
{
    public class StringListConfigurationItemViewModel : AbstractConfigurationItemViewModel<StringListSettingValue>
    {
        private KeyValuePair<string, string> _value;

        public KeyValuePair<string,string> Value
        {
            get => _value;
            set => Set(ref _value, value);
        }

        public override object ResultValue => Value.Key;
        
        protected override void OnInit(object defaultValue)
        {
            Value = Setting.AvailableValues.SingleOrDefault(x => x.Key == (string)defaultValue);
        }
    }
}

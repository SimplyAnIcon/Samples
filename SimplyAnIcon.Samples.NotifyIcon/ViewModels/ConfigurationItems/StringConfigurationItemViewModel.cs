using SimplyAnIcon.Plugins.V1.Settings;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels.ConfigurationItems
{
    public class StringConfigurationItemViewModel : AbstractConfigurationItemViewModel<StringSettingValue>
    {
        private string _value;

        public string Value
        {
            get => _value;
            set => Set(ref _value, ApplyModifiers(value));
        }

        public override bool IsValid()
        {
            if (Value.Length < Setting.MinimumLength)
                return false;
            return base.IsValid();
        }

        public override object ResultValue => Value;

        protected override void OnInit(object defaultValue)
        {
            Value = ApplyModifiers((string)defaultValue);
        }

        private string ApplyModifiers(string value)
        {
            switch (Setting.StringType)
            {
                case StringSettingValue.StringTypeEnum.AllLower:
                    return value.ToLower();
                case StringSettingValue.StringTypeEnum.AllUpper:
                    return value.ToUpper();
            }

            return value;
        }
    }
}

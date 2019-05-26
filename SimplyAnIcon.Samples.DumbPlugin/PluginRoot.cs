using System;
using System.Collections.Generic;
using SimplyAnIcon.Plugins.V1;
using SimplyAnIcon.Plugins.V1.Settings;

namespace SimplyAnIcon.Samples.DumbPlugin
{
    public class PluginRoot : ISimplyAPlugin
    {
        public string Name => "DumbPlugin";
        public string Description => "I'm Dumb !";
        public Version Version => new Version(0, 0, 0, 42);

        public Dictionary<string, object> Settings { get; } = new Dictionary<string, object>
        {
            {"TextValue1", "ABCD1234" },
            {"TextValue2", "cat" },
            {"IntValue1", 42 },
            {"BoolValue1", true  }
        };

        public void OnInit(Dictionary<string, object> config)
        {
        }

        public void OnRefresh()
        {
        }

        public void OnDispose()
        {
            //MessageBox.Show("These are the saved settings: " + Environment.NewLine + string.Join(Environment.NewLine, Settings.Select(x => x.Key + ": " + x.Value.ToString())),"Dumby", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        IEnumerable<AbstractSettingValue> ISimplyAPlugin.ConfigurationItems => new AbstractSettingValue[]
        {
            new StringSettingValue("TextValue1", "Some dumb text")
            {
                MinimumLength = 3,
                MaximumLength = 20,
                StringType = StringSettingValue.StringTypeEnum.AllUpper
            },
            new StringListSettingValue("TextValue2", "Some dumb animal", new[]
            {
                new KeyValuePair<string, string>("dog", "A dog"),
                new KeyValuePair<string, string>("cat", "A cat"),
                new KeyValuePair<string, string>("turtle", "A turtle"),
                new KeyValuePair<string, string>("elephant", "An elephant"),
                new KeyValuePair<string, string>("mouse", "A mouse"),
            }),
            new IntSettingValue("IntValue1", "Some dumb number")
            {
                Minimum = 21, Maximum = 84
            },
            new BoolSettingValue("BoolValue1", "Some dumb bool"),
        };

        public object GetConfigurationValue(string key)
        {
            return Settings[key];
        }

        public void SetConfigurationValue(string key, object value)
        {
            Settings[key] = value;
        }
    }
}

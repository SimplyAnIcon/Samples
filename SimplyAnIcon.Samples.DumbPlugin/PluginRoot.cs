using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SimplyAnIcon.Plugins.V1;
using SimplyAnIcon.Plugins.V1.Settings;

namespace SimplyAnIcon.Samples.DumbPlugin
{
    public class PluginRoot : ISimplyAPlugin
    {
        public string Name => "DumbPlugin";
        public string Description => "I'm Dumb !";
        public Version Version => new Version(0,0,0,42);
        public void OnInit()
        {
        }

        public void OnRefresh()
        {
        }

        public void OnDispose()
        {
            MessageBox.Show("Too dumb to be disposed","Dumby", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        IEnumerable<AbstractSettingValue> ISimplyAPlugin.ConfigurationItems => new List<AbstractSettingValue>();

        public object GetConfigurationValue(string key)
        {
            throw new NotImplementedException();
        }

        public void SetConfigurationValue(string key, object value)
        {
            throw new NotImplementedException();
        }
    }
}

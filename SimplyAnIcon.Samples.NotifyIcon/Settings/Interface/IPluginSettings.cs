using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyAnIcon.Samples.NotifyIcon.Models;

namespace SimplyAnIcon.Samples.NotifyIcon.Settings.Interface
{
    public interface IPluginSettings
    {
        bool IsActive(string pluginName);

        void SetActivationStatus(string pluginName, bool value);

        void AddPlugin(string name);

        IEnumerable<PluginSettingEntry> GetPlugins();
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimplyAnIcon.Samples.NotifyIcon.Helpers.Interfaces;
using SimplyAnIcon.Samples.NotifyIcon.Models;
using SimplyAnIcon.Samples.NotifyIcon.Settings.Interface;

namespace SimplyAnIcon.Samples.NotifyIcon.Settings
{
    public class PluginSettings : IPluginSettings
    {
        private readonly IWindowsHelper _windowsHelper;
        private readonly IJsonHelper _jsonHelper;

        public PluginSettings(IWindowsHelper windowsHelper, IJsonHelper jsonHelper)
        {
            _windowsHelper = windowsHelper;
            _jsonHelper = jsonHelper;
        }

        public bool IsActive(string pluginName)
        {
            var plugins = LoadPluginSettings().ToArray();

            var plugin = plugins.SingleOrDefault(p => p.Name == pluginName);

            return plugin?.IsActive ?? false;
        }

        public void SetActivationStatus(string pluginName, bool value)
        {
            var plugins = LoadPluginSettings().ToArray();

            var plugin = plugins.SingleOrDefault(p => p.Name == pluginName);

            if (plugin == null)
                return;

            plugin.IsActive = value;

            SavePluginSettings(plugins);
        }

        public void AddPlugin(string name)
        {
            var plugins = LoadPluginSettings().ToList();
            var entry = new PluginSettingEntry
            {
                Name = name,
                IsActive = false,
                Order = plugins.Any() ? plugins.Max(x => x.Order) + 1 : 0
            };
            plugins.Add(entry);
            SavePluginSettings(plugins);
        }

        public IEnumerable<PluginSettingEntry> GetPlugins()
        {
            return LoadPluginSettings().ToArray();
        }

        private IEnumerable<PluginSettingEntry> LoadPluginSettings()
        {
            var fi = new FileInfo(Path.Combine(_windowsHelper.AppRoamingDataPath(), nameof(PluginSettings) + ".json"));
            if (!fi.Exists)
                return new PluginSettingEntry[0];

            return _jsonHelper.DeserializeFile<IEnumerable<PluginSettingEntry>>(fi.FullName);
        }

        private void SavePluginSettings(IEnumerable<PluginSettingEntry> plugins)
        {
            _jsonHelper.SerializeToFile(plugins, Path.Combine(_windowsHelper.AppRoamingDataPath(), nameof(PluginSettings) + ".json"));
        }
    }
}

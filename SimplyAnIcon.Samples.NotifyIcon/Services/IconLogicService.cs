using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Com.Ericmas001.DependencyInjection.RegistrantFinders;
using SimplyAnIcon.Common.Models;
using SimplyAnIcon.Samples.NotifyIcon.Helpers;
using SimplyAnIcon.Samples.NotifyIcon.Services.Interfaces;
using SimplyAnIcon.Common.Services.Interfaces;
using SimplyAnIcon.Plugins.V1;
using SimplyAnIcon.Plugins.Wpf.V1;
using SimplyAnIcon.Plugins.Wpf.V1.MenuItemViewModels;
using SimplyAnIcon.Samples.NotifyIcon.Settings.Interface;

namespace SimplyAnIcon.Samples.NotifyIcon.Services
{
    public class IconLogicService : IIconLogicService
    {
        private readonly List<ISimplyAWpfPlugin> _pluginWpfInstances = new List<ISimplyAWpfPlugin>();
        private readonly List<ISimplyAPlugin> _pluginInstances = new List<ISimplyAPlugin>();
        private readonly IPluginService _pluginService;
        private readonly IPluginSettings _pluginSettings;

        public PluginCatalog PluginsCatalog => new PluginCatalog
        {
            PluginInstances = _pluginInstances,
            PluginWpfInstances = _pluginWpfInstances
        };
        
        public event EventHandler<IEnumerable<MenuItemViewModel>> OnMenuBuilt = delegate { };
        public event EventHandler OnAppExited = delegate { };

        public IconLogicService(IPluginService pluginService, IPluginSettings pluginSettings)
        {
            _pluginService = pluginService;
            _pluginSettings = pluginSettings;
        }

        public void UpdateIcon()
        {
            LoadPlugins();
            _pluginInstances.ForEach(x => x.OnRefresh());
            _pluginWpfInstances.ForEach(x => x.OnRefresh());
            OnMenuBuilt(this, BuildMenu());
            BuildMenu();
        }
        public void Restart()
        {
            Process.Start(Assembly.GetEntryAssembly().Location);
            OnAppExited(this, new EventArgs());
        }

        public void OnDispose()
        {
            _pluginInstances.ForEach(x => x.OnDispose());
            _pluginWpfInstances.ForEach(x => x.OnDispose());
        }
        private void LoadPlugins()
        {
            var registrantBuilder = new RegistrantFinderBuilder()
                .AddAssemblyPrefix("IconeIA.Core");

            var plugins = _pluginService.LoadPlugins(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Plugins"), new UnityInstanceResolverHelper(), registrantBuilder);

            var settings = _pluginSettings.GetPlugins().ToArray();
            foreach (var plugin in plugins.PluginInstances.Concat(plugins.PluginWpfInstances))
            {
                if (!settings.Any(x => x.Name.Equals(plugin.Name, StringComparison.InvariantCultureIgnoreCase)))
                    _pluginSettings.AddPlugin(plugin.Name);
            }

            _pluginInstances.Clear();
            _pluginInstances.AddRange(plugins.PluginInstances);

            _pluginWpfInstances.Clear();
            _pluginWpfInstances.AddRange(plugins.PluginWpfInstances);

            foreach (var resourceDictionary in _pluginWpfInstances.SelectMany(x => x.ResourceDictionaries))
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            _pluginInstances.ForEach(x => x.OnInit());
            _pluginWpfInstances.ForEach(x => x.OnInit());
        }

        private IEnumerable<MenuItemViewModel> BuildMenu()
        {
            var items = new List<MenuItemViewModel>();
            var itemsOfPlugin = _pluginWpfInstances.Select(x => x.MenuItems).ToList();
            var first = true;
            foreach (var it in itemsOfPlugin)
            {
                if (first)
                    first = false;
                else
                    items.Add(new SeparatorMenuItemViewModel());
                items.AddRange(it.ToList());
            }

            return items;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Com.Ericmas001.DependencyInjection.RegistrantFinders;
using SimplyAnIcon.Common.Helpers.Interfaces;
using SimplyAnIcon.Common.Models;
using SimplyAnIcon.Common.Services.Interfaces;
using SimplyAnIcon.Plugins.Wpf.V1.MenuItemViewModels;
using SimplyAnIcon.Samples.NotifyIcon.Helpers;
using SimplyAnIcon.Samples.NotifyIcon.Services.Interfaces;

namespace SimplyAnIcon.Samples.NotifyIcon.Services
{
    public class IconLogicService : IIconLogicService
    {
        private readonly IPluginService _pluginService;
        private readonly IPluginBasicConfigHelper _pluginBasicConfigHelper;

        public IEnumerable<PluginInfo> PluginsCatalog { get; private set; }

        public event EventHandler OnAppExited = delegate { };

        public IconLogicService(IPluginService pluginService, IPluginBasicConfigHelper pluginBasicConfigHelper)
        {
            _pluginService = pluginService;
            _pluginBasicConfigHelper = pluginBasicConfigHelper;
        }

        public async Task<IEnumerable<MenuItemViewModel>> UpdateIcon()
        {
            LoadPlugins();
            PluginsCatalog.Where(x => x.IsActivated).ToList().ForEach(x => x.Plugin.OnRefresh());

            return await Task.Run(() =>
            {
                return BuildMenu();
            });
        }
        public void Restart()
        {
            Process.Start(Assembly.GetEntryAssembly().Location);
            OnAppExited(this, new EventArgs());
        }

        public void OnDispose()
        {
            PluginsCatalog.Where(x => x.IsActivated).ToList().ForEach(x => x.Plugin.OnDeactivation());
            PluginsCatalog.ToList().ForEach(x => x.Plugin.OnDispose());
        }
        private void LoadPlugins()
        {
            var registrantBuilder = new RegistrantFinderBuilder()
                .AddAssemblyPrefix("IconeIA.Core");

            var pluginPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Plugins");
            if (!Directory.Exists(pluginPath))
                Directory.CreateDirectory(pluginPath);

            PluginsCatalog = _pluginService.LoadPlugins(PluginsCatalog, new[] { pluginPath }, new UnityInstanceResolverHelper(), registrantBuilder, _pluginBasicConfigHelper.GetForcedPlugins());

            foreach (var resourceDictionary in PluginsCatalog.Where(x => x.IsNew && x.IsForeground).SelectMany(x => x.ForegroundPlugin.ResourceDictionaries))
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            foreach (var plugin in PluginsCatalog.Where(x => x.IsNew && x.IsActivated))
                plugin.Plugin.OnActivation();
        }

        private IEnumerable<MenuItemViewModel> BuildMenu()
        {
            var items = new List<MenuItemViewModel>();
            var itemsOfPlugin = PluginsCatalog.Where(x => x.IsActivated && x.IsForeground).Select(x => x.ForegroundPlugin.MenuItems).ToList();
            var first = true;
            foreach (var it in itemsOfPlugin)
            {
                if (first)
                    first = false;
                else
                    items.Add(new SeparatorMenuItemViewModel(null));
                items.AddRange(it.ToList());
            }

            return items;
        }
    }
}

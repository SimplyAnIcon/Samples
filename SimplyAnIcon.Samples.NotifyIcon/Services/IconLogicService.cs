using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Com.Ericmas001.DependencyInjection.RegistrantFinders;
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

        public PluginCatalog PluginsCatalog { get; private set; }

        public event EventHandler OnAppExited = delegate { };

        public IconLogicService(IPluginService pluginService)
        {
            _pluginService = pluginService;
        }

        public async Task<IEnumerable<MenuItemViewModel>> UpdateIcon()
        {
            LoadPlugins();
            PluginsCatalog.ActiveBackgroungPlugins.ToList().ForEach(x => x.OnRefresh());
            PluginsCatalog.ActiveForegroundPlugins.ToList().ForEach(x => x.OnRefresh());

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
            PluginsCatalog.ActiveBackgroungPlugins.ToList().ForEach(x => x.OnDispose());
            PluginsCatalog.ActiveForegroundPlugins.ToList().ForEach(x => x.OnDispose());
        }
        private void LoadPlugins()
        {
            var registrantBuilder = new RegistrantFinderBuilder()
                .AddAssemblyPrefix("IconeIA.Core");

            PluginsCatalog = _pluginService.LoadPlugins(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Plugins"), new UnityInstanceResolverHelper(), registrantBuilder);

            foreach (var resourceDictionary in PluginsCatalog.ActiveForegroundPlugins.SelectMany(x => x.ResourceDictionaries))
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            var config = new Dictionary<string, object>();

            PluginsCatalog.ActiveBackgroungPlugins.ToList().ForEach(x => x.OnInit(config));
            PluginsCatalog.ActiveForegroundPlugins.ToList().ForEach(x => x.OnInit(config));
        }

        private IEnumerable<MenuItemViewModel> BuildMenu()
        {
            var items = new List<MenuItemViewModel>();
            var itemsOfPlugin = PluginsCatalog.ActiveForegroundPlugins.Select(x => x.MenuItems).ToList();
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

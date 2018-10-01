using System;
using System.Collections.Generic;
using SimplyAnIcon.Common.Models;
using SimplyAnIcon.Plugins.Wpf.V1.MenuItemViewModels;

namespace SimplyAnIcon.Samples.NotifyIcon.Services.Interfaces
{
    public interface IIconLogicService
    {
        event EventHandler<IEnumerable<MenuItemViewModel>> OnMenuBuilt;
        event EventHandler OnAppExited;

        PluginCatalog PluginsCatalog { get; }

        void UpdateIcon();

        void Restart();

        void OnDispose();
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimplyAnIcon.Common.Models;
using SimplyAnIcon.Plugins.Wpf.V1.MenuItemViewModels;

namespace SimplyAnIcon.Samples.NotifyIcon.Services.Interfaces
{
    public interface IIconLogicService
    {
        event EventHandler OnAppExited;

        PluginCatalog PluginsCatalog { get; }

        Task<IEnumerable<MenuItemViewModel>> UpdateIcon();

        void Restart();

        void OnDispose();
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimplyAnIcon.Core.Models;
using SimplyAnIcon.Plugins.Wpf.V1.MenuItemViewModels;

namespace SimplyAnIcon.Samples.NotifyIcon.Services.Interfaces
{
    public interface IIconLogicService
    {
        event EventHandler OnAppExited;

        IEnumerable<PluginInfo> PluginsCatalog { get; }

        Task<IEnumerable<MenuItemViewModel>> UpdateIcon();

        void Restart();

        void OnDispose();
    }
}

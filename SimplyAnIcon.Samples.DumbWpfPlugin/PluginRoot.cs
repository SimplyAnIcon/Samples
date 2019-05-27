using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Com.Ericmas001.Mvvm;
using SimplyAnIcon.Plugins.V1;
using SimplyAnIcon.Plugins.V1.Settings;
using SimplyAnIcon.Plugins.Wpf.V1;
using SimplyAnIcon.Plugins.Wpf.V1.MenuItemViewModels;

namespace SimplyAnIcon.Samples.DumbWpfPlugin
{
    public class PluginRoot : ISimplyAWpfPlugin
    {
        public string Name => "Dumb Wpf Plugin";
        public string Description => "I'm a Dumb Wpf Plugin";
        public Version Version => new Version(0, 0, 0, 42);

        IEnumerable<AbstractSettingValue> ISimplyAPlugin.ConfigurationItems => new List<AbstractSettingValue>();
        private MenuItemViewModel _menuAction;
        private MenuItemViewModel _menuItems;
        private int _currentDumb;

        public void OnInit(Dictionary<string, object> config)
        {
        }

        public void OnActivation()
        {
            _menuItems = new MenuItemViewModel(null)
            {
                Name = "Dumb Items",
                Children = { new SeparatorMenuItemViewModel(null) },
                IconPath = Application.Current.Resources["icon_dumb"]
            };
            _menuAction = new MenuItemViewModel(null)
            {
                Name = "Dumb Actions",
                IconPath = Application.Current.Resources["icon_dumb"],
                Children =
                {
                    new MenuItemViewModel(null)
                    {
                        Name = "Add To Top",
                        StaysOpenOnClick = true,
                        Action = new RelayCommand(() => _menuItems.Children.Insert(0, new SelectableMenuItem(null)
                        {
                            Name = $"Dummy #{_currentDumb++:00}"
                        })),
                        IconPath = Application.Current.Resources["icon_dumb"]
                    },
                    new MenuItemViewModel(null)
                    {
                        Name = "Select Top",
                        StaysOpenOnClick = true,
                        Action = new RelayCommand(() =>
                        {
                            _menuItems.Children.OfType<SelectableMenuItem>().ToList().ForEach(x => x.IsSelected = false);
                            _menuItems.Children.OfType<SelectableMenuItem>().First().IsSelected = true;
                        }, () => _menuItems.Children.First() is SelectableMenuItem),
                        IconPath = Application.Current.Resources["icon_dumb"]
                    },
                    new MenuItemViewModel(null)
                    {
                        Name = "Remove Top",
                        StaysOpenOnClick = true,
                        Action = new RelayCommand(() => _menuItems.Children.Remove(_menuItems.Children.First()), () => _menuItems.Children.First() is SelectableMenuItem),
                        IconPath = Application.Current.Resources["icon_dumb"]
                    },
                    new SeparatorMenuItemViewModel(null),
                    new AddingSelectableMenuItem(null,
                        s => _menuItems.Children.Insert(0, new SelectableMenuItem(null)
                        {
                            Name = s
                        }),
                        s => _menuItems.Children.Add(new SelectableMenuItem(null)
                        {
                            Name = s
                        })),
                    new SeparatorMenuItemViewModel(null),
                    new MenuItemViewModel(null)
                    {
                        Name = "Add To Bottom",
                        StaysOpenOnClick = true,
                        Action = new RelayCommand(() => _menuItems.Children.Add(new SelectableMenuItem(null)
                        {
                            Name = $"Dummy #{_currentDumb++:00}"
                        })),
                        IconPath = Application.Current.Resources["icon_dumb"]
                    },
                    new MenuItemViewModel(null)
                    {
                        Name = "Select Bottom",
                        StaysOpenOnClick = true,
                        Action = new RelayCommand(() =>
                        {
                            _menuItems.Children.OfType<SelectableMenuItem>().ToList().ForEach(x => x.IsSelected = false);
                            _menuItems.Children.OfType<SelectableMenuItem>().Last().IsSelected = true;
                        }, () => _menuItems.Children.Last() is SelectableMenuItem),
                        IconPath = Application.Current.Resources["icon_dumb"]
                    },
                    new MenuItemViewModel(null)
                    {
                        Name = "Remove Bottom",
                        StaysOpenOnClick = true,
                        Action = new RelayCommand(() => _menuItems.Children.Remove(_menuItems.Children.Last()), () => _menuItems.Children.Last() is SelectableMenuItem),
                        IconPath = Application.Current.Resources["icon_dumb"]
                    }
                }
            };
        }

        public void OnDeactivation()
        {
        }

        public void OnRefresh()
        {
        }

        public void OnDispose()
        {
        }

        public object GetConfigurationValue(string key)
        {
            throw new NotImplementedException();
        }

        public void SetConfigurationValue(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void OnConfigSave()
        {
            throw new NotImplementedException();
        }

        public UserControl CustomConfigControl => null;

        public IEnumerable<MenuItemViewModel> MenuItems => new[] {
            _menuAction,
            _menuItems,
            new MenuItemViewModel(null)
            {
                Name = "Dumb Click",
                StaysOpenOnClick = true,
                IconPath = Application.Current.Resources["icon_dumb"]
            }
        };
        public IEnumerable<ResourceDictionary> ResourceDictionaries => new[] { new CustomDictionary() };
    }
}

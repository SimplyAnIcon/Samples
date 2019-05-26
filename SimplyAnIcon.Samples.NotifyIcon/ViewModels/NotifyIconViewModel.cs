using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Com.Ericmas001.Mvvm;
using Com.Ericmas001.Mvvm.Collections;
using SimplyAnIcon.Common.ViewModels.Interfaces;
using SimplyAnIcon.Common.Windows;
using SimplyAnIcon.Plugins.Wpf.V1.MenuItemViewModels;
using SimplyAnIcon.Samples.NotifyIcon.Services.Interfaces;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels
{
    public class NotifyIconViewModel : ViewModelBase, ISimplyAnIconViewModel
    {
        private bool _isVisible;
        private readonly IIconLogicService _logic;
        private readonly IResolverService _resolverService;
        private bool _stayOpen;
        private List<MenuItemViewModel> _permanentBottomItems;

        public FastObservableCollection<MenuItemViewModel> Items { get; } = new FastObservableCollection<MenuItemViewModel>();
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                Set(ref _isVisible, value);
                RaisePropertyChanged(nameof(IconSource));
                RaisePropertyChanged(nameof(IconName));
                RaisePropertyChanged(nameof(Items));
            }
        }

        public string IconSource => "/cool.ico";
        public string IconName => IsVisible ? "SimplyAnIcon" : "SimplyAnIcon (Updating ...)";

        public bool StayOpen
        {
            get => _stayOpen;
            set => Set(ref _stayOpen, value);
        }

        public NotifyIconViewModel(IIconLogicService logic, IResolverService resolverService)
        {
            _logic = logic;
            _resolverService = resolverService;

            _permanentBottomItems = new List<MenuItemViewModel>
            {
                new SeparatorMenuItemViewModel(null),
                new MenuItemViewModel(null)
                {
                    Name = "Update",
                    Action = new RelayCommand(async () => await UpdateIcon()),
                    IconPath = Application.Current.Resources["SimplyIconUpdate"]
                },
                new MenuItemViewModel(null)
                {
                    Name = "Options",
                    Action = new RelayCommand(StartConfigWindow),
                    IconPath = Application.Current.Resources["SimplyIconConfig"]
                },
                new SeparatorMenuItemViewModel(null),
                new MenuItemViewModel(null)
                {
                    Name = "Restart",
                    Action = new RelayCommand(_logic.Restart),
                    IconPath = Application.Current.Resources["SimplyIconRestart"]
                },
                new MenuItemViewModel(null)
                {
                    Name = "Exit",
                    Action = new RelayCommand(KillIcon),
                    IconPath = Application.Current.Resources["SimplyIconExit"]
                }
            };

            _logic.OnAppExited += (s, e) => KillIcon();
        }
        public async Task LoadIcon()
        {
            await UpdateIcon();
        }

        private async Task UpdateIcon()
        {
            IsVisible = false;

            var newItems = await _logic.UpdateIcon();

            Items.Clear();
            var addedList = newItems.ToList();
            Items.AddItems(addedList);
            Items.AddItems(_permanentBottomItems);
            IsVisible = true;
        }

        private void StartConfigWindow()
        {
            var confVm = _resolverService.Resolve<ConfigViewModel>();
            confVm.OnInit(_logic.PluginsCatalog);
            var window = new ConfigWindow(confVm);
            window.Closed += async (sender, args) => await UpdateIcon();
            window.Show();
        }

        private void OnForceMenuOpen(object sender, bool e)
        {
            StayOpen = e;
        }

        private void KillIcon()
        {
            IsVisible = false;
            _logic.OnDispose();
            Application.Current.Shutdown();
        }
    }
}

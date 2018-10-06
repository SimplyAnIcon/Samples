using System.Windows;
using SimplyAnIcon.Plugins.Wpf.V1.MenuItemViewModels;

namespace SimplyAnIcon.Samples.DumbWpfPlugin
{
    public class SelectableMenuItem : MenuItemViewModel
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }

        public SelectableMenuItem()
        {
            ItemStyle = (Style)Application.Current.FindResource("DumbSelectableMenuItemStyle");
        }
    }
}

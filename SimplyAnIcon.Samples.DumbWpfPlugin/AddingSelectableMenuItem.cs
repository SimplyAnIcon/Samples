using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimplyAnIcon.Plugins.Wpf.V1.MenuItemViewModels;

namespace SimplyAnIcon.Samples.DumbWpfPlugin
{
    public class AddingSelectableMenuItem : MenuItemViewModel
    {
        private readonly Action<string> _fctAddTop;
        private readonly Action<string> _fctAddBottom;

        private string _text;
        private ICommand _addTopCommand;
        private ICommand _addBottomCommand;

        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        public ICommand AddTopCommand => _addTopCommand ?? (_addTopCommand = new RelayCommand(() =>
        {
            _fctAddTop(_text);
            Text = "";
        }, () => !string.IsNullOrWhiteSpace(_text)));

        public ICommand AddBottomCommand => _addBottomCommand ?? (_addBottomCommand = new RelayCommand(() =>
        {
            _fctAddBottom(_text);
            Text = "";
        }, () => !string.IsNullOrWhiteSpace(_text)));

        public AddingSelectableMenuItem(MenuItemViewModel parent, Action<string> fctAddTop, Action<string> fctAddBottom) : base(parent)
        {
            _fctAddTop = fctAddTop;
            _fctAddBottom = fctAddBottom;
            Height = 60;
            StaysOpenOnClick = true;
        }
    }
}

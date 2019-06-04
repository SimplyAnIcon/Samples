using SimplyAnIcon.Core.Helpers.Interfaces;
using SimplyAnIcon.Core.Services.Interfaces;
using SimplyAnIcon.Core.ViewModels;
using SimplyAnIcon.Core.ViewModels.Interfaces;

namespace SimplyAnIcon.Samples.NotifyIcon.ViewModels
{
    public class NotifyIconViewModel : AbstractNotifyIconViewModel
    {
        public override string IconSource => "/cool.ico";
        public override string IconName => IsVisible ? "SimplyAnIcon" : "SimplyAnIcon (Updating ...)";

        public NotifyIconViewModel(IIconLogicService logic, IViewModelFactory viewModelFactory, IIconConfigHelper iconConfigHelper)
            : base(logic, viewModelFactory, iconConfigHelper)
        {
        }
    }
}

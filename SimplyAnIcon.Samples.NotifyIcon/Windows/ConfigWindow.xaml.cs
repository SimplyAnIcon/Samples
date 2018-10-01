using SimplyAnIcon.Samples.NotifyIcon.ViewModels;

namespace SimplyAnIcon.Samples.NotifyIcon.Windows
{
    public partial class ConfigWindow
    {
        public ConfigWindow( ConfigViewModel configViewModel )
        {
            InitializeComponent();
            DataContext = configViewModel;
        }
    }
}

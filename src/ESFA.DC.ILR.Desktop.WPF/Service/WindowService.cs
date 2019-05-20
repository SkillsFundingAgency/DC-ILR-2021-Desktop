using ESFA.DC.ILR.Desktop.WPF.Common;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Views;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class WindowService : IWindowService
    {
        public void ShowSettingsWindow()
        {
            new SettingsWindow().ShowDialog();
        }

        public void ShowAboutWindow()
        {
            new AboutWindow().ShowDialog();
        }
    }
}

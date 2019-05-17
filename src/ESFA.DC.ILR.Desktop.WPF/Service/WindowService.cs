using ESFA.DC.ILR.Desktop.WPF.Common;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Views;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class WindowService : IWindowService
    {
        public void ShowSettingsWindow(WindowEnum windowToShow)
        {
            switch (windowToShow)
            {
                case WindowEnum.SettingsWindow:
                    new SettingsWindow().ShowDialog();
                    break;
                case WindowEnum.AboutWindow:
                    new AboutWindow().ShowDialog();
                    break;
                default:
                    break;
            }
        }
    }
}

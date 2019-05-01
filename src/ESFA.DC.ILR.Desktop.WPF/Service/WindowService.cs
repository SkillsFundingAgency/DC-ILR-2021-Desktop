using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class WindowService : IWindowService
    {
        public void ShowSettingsWindow()
        {
            new SettingsWindow().ShowDialog();
        }
    }
}

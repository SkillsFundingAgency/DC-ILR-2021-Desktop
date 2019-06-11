using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class WindowsProcessService : IWindowsProcessService
    {
        public void ProcessStart(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
    }
}

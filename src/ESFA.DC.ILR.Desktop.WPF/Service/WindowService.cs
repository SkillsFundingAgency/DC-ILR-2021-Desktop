using System;
using System.Windows;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Views;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class WindowService : IWindowService
    {
        public void ShowSettingsWindow()
        {
            var settingsWindow = new SettingsWindow()
            {
                Owner = Application.Current.MainWindow,
            };

            settingsWindow.ShowDialog();
        }

        public void ShowAboutWindow()
        {
            var aboutWindow = new AboutWindow()
            {
                Owner = Application.Current.MainWindow,
            };

            aboutWindow.ShowDialog();
        }

        public void ShowReportFiltersWindow()
        {
            var reportFiltersWindow = new ReportFiltersWindow()
            {
                Owner = Application.Current.MainWindow,
            };

            reportFiltersWindow.ShowDialog();
        }
    }
}

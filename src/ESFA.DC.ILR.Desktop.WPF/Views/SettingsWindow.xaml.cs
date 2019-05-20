using System.Windows;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml.
    /// </summary>
    public partial class SettingsWindow : Window, ICloseable
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }
    }
}

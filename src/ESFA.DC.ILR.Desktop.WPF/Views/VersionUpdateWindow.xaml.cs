using System.Windows;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Views
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml.
    /// </summary>
    public partial class VersionUpdateWindow : Window, ICloseable
    {
        public VersionUpdateWindow()
        {
            InitializeComponent();
        }
    }
}

using System.Windows;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Views
{
    /// <summary>
    /// Interaction logic for ReportFiltersWindow.xaml
    /// </summary>
    public partial class ReportFiltersWindow : Window, ICloseable
    {
        public ReportFiltersWindow()
        {
            InitializeComponent();
        }
    }
}

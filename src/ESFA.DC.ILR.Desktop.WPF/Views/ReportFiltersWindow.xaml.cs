using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

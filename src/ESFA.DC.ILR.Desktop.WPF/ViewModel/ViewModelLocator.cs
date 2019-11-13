/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ESFA.DC.ILR.Desktop.WPF"
                           x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public AboutViewModel About => ServiceLocator.Current.GetInstance<AboutViewModel>();

        public SettingsViewModel Settings => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public ReportFiltersViewModel ReportFilters => ServiceLocator.Current.GetInstance<ReportFiltersViewModel>();

        public VersionUpdateViewModel Update => ServiceLocator.Current.GetInstance<VersionUpdateViewModel>();

        public static void Cleanup()
        {
        }
    }
}
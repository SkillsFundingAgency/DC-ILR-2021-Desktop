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

using System.Threading;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Stubs;
using ESFA.DC.ILR.Desktop.Stubs.Tasks;
using ESFA.DC.ILR.Desktop.WPF.Service;
using GalaSoft.MvvmLight;
using System.Windows.Navigation;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var containerBuilder = new ContainerBuilder();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // register Design Time Services
            }
            else
            {
                // register Production Services
            }

            containerBuilder.RegisterType<MessengerService>().As<IMessengerService>().SingleInstance();

            containerBuilder.RegisterType<WindowService>().As<IWindowService>().SingleInstance();

            containerBuilder.RegisterType<SettingsServiceStub>().As<ISettingsService>();
            
            containerBuilder.RegisterType<MainViewModel>().SingleInstance();
            containerBuilder.RegisterType<SettingsViewModel>().SingleInstance();

            containerBuilder.RegisterType<IlrDesktopServiceStub>().As<IIlrDesktopService>();
            containerBuilder.RegisterType<DesktopTaskStub>().As<IDesktopTask>();
            
            var container = containerBuilder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public SettingsViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
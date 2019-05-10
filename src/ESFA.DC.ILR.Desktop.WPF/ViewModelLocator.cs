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

using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using ESFA.DC.ILR.Desktop.WPF.Modules;
using ESFA.DC.ILR.Desktop.WPF.ViewModels;

namespace ESFA.DC.ILR.Desktop.WPF
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var containerBuilder = BuildContainerBuilder();
            var container = containerBuilder.Build();
            var reg = container.IsRegistered<SettingsViewModels>();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }

        private ContainerBuilder BuildContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<DesktopServicesModule>();
            containerBuilder.RegisterModule<ViewModelsModule>();
            containerBuilder.RegisterModule<IlrServicesModule>();

            // Common Service Registration
            containerBuilder.RegisterModule<LoggingModule>();
            containerBuilder.RegisterModule<SerializationModule>();
            containerBuilder.RegisterModule<IOModule>();           

            return containerBuilder;
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
                return ServiceLocator.Current.GetInstance<SettingsViewModels>();
            }
        }
        
        public static void Cleanup()
        {
        }
    }
}
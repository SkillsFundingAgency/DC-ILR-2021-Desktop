using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.Tests.TestSpecificSubClasses
{
    public class VersionUpdateViewModelTestSpecificTestClass : VersionUpdateViewModel
    {
        public VersionUpdateViewModelTestSpecificTestClass(
            IMessengerService messengerService,
            IWindowsProcessService windowsProcessService)
            : base(messengerService, windowsProcessService)
        {
        }

        public new void Initialize(VersionMessage message)
        {
            base.Initialize(message);
        }
    }
}
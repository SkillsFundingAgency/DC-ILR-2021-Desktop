using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.Tests
{
    public class SettingsViewModelTests
    {
        [Fact]
        public void ChooseOutputDirectoryCommandExecute()
        {
            string folderName = "FolderName";

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();
            dialogInteractionServiceMock.Setup(x => x.GetFolderNameFromFolderBrowserDialog(It.IsAny<string>(), It.IsAny<string>())).Returns(folderName);

            var settingsServiceMock = new Mock<IDesktopServiceSettings>();
            var settingsVM = TestSettingsViewModel(dialogInteractionService: dialogInteractionServiceMock.Object);

            settingsVM.ChooseOutputDirectoryCommand.Execute(null);
            settingsVM.OutputDirectory.Should().Be(folderName);
        }

        [Fact]
        public async Task SaveSettings()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.SetupAllProperties();
            desktopServiceSettingsMock.SetupGet(x => x.IlrDatabaseConnectionString).Returns("ConnectionStringValue");
            desktopServiceSettingsMock.SetupGet(x => x.OutputDirectory).Returns("OutputDirectoryValue");

            var vm = TestSettingsViewModel(desktopServiceSettings: desktopServiceSettingsMock.Object);

            await vm.SaveSettingsCommand.ExecuteAsync();
            vm.CanExecute().Should().BeTrue();
        }

        private SettingsViewModel TestSettingsViewModel(IDesktopServiceSettings desktopServiceSettings = null, IDialogInteractionService dialogInteractionService = null)
        {
            return new SettingsViewModel(desktopServiceSettings ?? Mock.Of<IDesktopServiceSettings>(), dialogInteractionService ?? Mock.Of<IDialogInteractionService>());
        }
    }
}

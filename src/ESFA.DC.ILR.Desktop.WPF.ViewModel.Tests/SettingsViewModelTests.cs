using System.Threading;
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
            string outputDirectoryDescription = "Description_for_OutputDirectory";

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();
            dialogInteractionServiceMock.Setup(x => x.GetFolderNameFromFolderBrowserDialog(It.IsAny<string>(), It.IsAny<string>())).Returns(folderName);

            var settingsVM = TestSettingsViewModel(dialogInteractionService: dialogInteractionServiceMock.Object);

            settingsVM.ChooseOutputDirectoryCommand.Execute(null);
            var result = settingsVM.OutputDirectory;
            result.Should().Be(folderName);
        }

        [Fact]
        public async Task SaveCommandExecute()
        {
            var cancellationToken = CancellationToken.None;
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();

            var vm = TestSettingsViewModel(desktopServiceSettings: desktopServiceSettingsMock.Object);

            desktopServiceSettingsMock.Setup(x => x.SaveAsync(cancellationToken))
                .Callback(() => vm.CanExecute().Should().BeTrue())
                .Returns(Task.CompletedTask);

            var result = vm.SaveSettingsCommand.ExecuteAsync().GetAwaiter();
            result.IsCompleted.Should().BeTrue();
        }

        [Fact]
        public async Task SaveCommandExecute_EmptyConnectionString()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.SetupAllProperties();
            desktopServiceSettingsMock.SetupGet(x => x.IlrDatabaseConnectionString).Returns(string.Empty);
            desktopServiceSettingsMock.SetupGet(x => x.OutputDirectory).Returns("OutputDirectory");

            var vm = TestSettingsViewModel(desktopServiceSettings: desktopServiceSettingsMock.Object);
            vm.CanExecute().Should().BeFalse();
        }

        [Fact]
        public async Task SaveCommandExecut_EmptyString()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.SetupAllProperties();
            desktopServiceSettingsMock.SetupGet(x => x.IlrDatabaseConnectionString).Returns("ConnectionStringValue");
            desktopServiceSettingsMock.SetupGet(x => x.OutputDirectory).Returns(string.Empty);

            var vm = TestSettingsViewModel(desktopServiceSettings: desktopServiceSettingsMock.Object);
            vm.CanExecute().Should().BeFalse();
        }

        [Fact]
        public async Task SaveCommandExecute_WhiteSpace()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.SetupAllProperties();
            desktopServiceSettingsMock.SetupGet(x => x.IlrDatabaseConnectionString).Returns("ConnectionStringValue");
            desktopServiceSettingsMock.SetupGet(x => x.OutputDirectory).Returns("  ");

            var vm = TestSettingsViewModel(desktopServiceSettings: desktopServiceSettingsMock.Object);
            vm.CanExecute().Should().BeFalse();
        }

        [Fact]
        public async Task SaveCommandExecute_NullValues()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();

            desktopServiceSettingsMock.SetupGet(x => x.IlrDatabaseConnectionString).Returns(null as string);
            desktopServiceSettingsMock.SetupGet(x => x.OutputDirectory).Returns(null as string);

            var vm = TestSettingsViewModel(desktopServiceSettings: desktopServiceSettingsMock.Object);
            vm.CanExecute().Should().BeFalse();
        }

        private SettingsViewModel TestSettingsViewModel(IDesktopServiceSettings desktopServiceSettings = null, IDialogInteractionService dialogInteractionService = null)
        {
            return new SettingsViewModel(desktopServiceSettings ?? Mock.Of<IDesktopServiceSettings>(), dialogInteractionService ?? Mock.Of<IDialogInteractionService>());
        }
    }
}

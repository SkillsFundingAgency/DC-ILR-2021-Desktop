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
            string outputDirectoryDescription = "Choose Output Directory";

            string currentDirectoryName = "CurrentDirectory";

            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock
                .SetupSequence(s => s.OutputDirectory)
                .Returns(currentDirectoryName)
                .Returns(folderName);

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();
            dialogInteractionServiceMock.Setup(x => x.GetFolderNameFromFolderBrowserDialog(currentDirectoryName, outputDirectoryDescription)).Returns(folderName);

            var viewModel = NewViewModel(desktopServiceSettingsMock.Object, dialogInteractionServiceMock.Object);

            viewModel.ChooseOutputDirectoryCommand.Execute(null);

            viewModel.OutputDirectory.Should().Be(folderName);
        }

        [Fact]
        public async Task SaveCommand_Execute()
        {
            var cancellationToken = CancellationToken.None;
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            var closeableMock = new Mock<ICloseable>();

            desktopServiceSettingsMock.SetupGet(x => x.ExportToSql).Returns(true);
            desktopServiceSettingsMock.SetupGet(s => s.IlrDatabaseConnectionString).Returns("not empty");
            desktopServiceSettingsMock.SetupGet(s => s.OutputDirectory).Returns("not empty either");

            var vm = NewViewModel(desktopServiceSettingsMock.Object);

            await vm.SaveSettingsCommand.ExecuteAsync(closeableMock.Object);

            desktopServiceSettingsMock.Verify(ds => ds.SaveAsync(It.IsAny<CancellationToken>()));
            closeableMock.Verify(c => c.Close());
        }

        [Fact]
        public async Task CanSave_EmptyConnectionString()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.SetupAllProperties();
            desktopServiceSettingsMock.SetupGet(x => x.ExportToSql).Returns(true);
            desktopServiceSettingsMock.SetupGet(x => x.IlrDatabaseConnectionString).Returns(string.Empty);
            desktopServiceSettingsMock.SetupGet(x => x.OutputDirectory).Returns("OutputDirectory");

            var vm = NewViewModel(desktopServiceSettings: desktopServiceSettingsMock.Object);
            vm.CanSave().Should().BeFalse();
        }

        [Fact]
        public async Task CanSave_EmptyString()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.SetupAllProperties();
            desktopServiceSettingsMock.SetupGet(x => x.ExportToSql).Returns(true);
            desktopServiceSettingsMock.SetupGet(x => x.IlrDatabaseConnectionString).Returns("ConnectionStringValue");
            desktopServiceSettingsMock.SetupGet(x => x.OutputDirectory).Returns(string.Empty);

            var vm = NewViewModel(desktopServiceSettings: desktopServiceSettingsMock.Object);
            vm.CanSave().Should().BeFalse();
        }

        [Fact]
        public async Task CanSave_WhiteSpace()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.SetupAllProperties();
            desktopServiceSettingsMock.SetupGet(x => x.ExportToSql).Returns(true);
            desktopServiceSettingsMock.SetupGet(x => x.IlrDatabaseConnectionString).Returns("ConnectionStringValue");
            desktopServiceSettingsMock.SetupGet(x => x.OutputDirectory).Returns("  ");

            var vm = NewViewModel(desktopServiceSettings: desktopServiceSettingsMock.Object);
            vm.CanSave().Should().BeFalse();
        }

        [Fact]
        public async Task CanSave_NullValues()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();

            desktopServiceSettingsMock.SetupGet(x => x.ExportToSql).Returns(true);
            desktopServiceSettingsMock.SetupGet(x => x.IlrDatabaseConnectionString).Returns(null as string);
            desktopServiceSettingsMock.SetupGet(x => x.OutputDirectory).Returns(null as string);

            var vm = NewViewModel(desktopServiceSettings: desktopServiceSettingsMock.Object);
            vm.CanSave().Should().BeFalse();
        }

        [Fact]
        public void CloseWindowCommandExecute()
        {
            var windowCloseable = new Mock<ICloseable>();

            NewViewModel().CloseWindowCommand.Execute(windowCloseable.Object);

            windowCloseable.Verify(c => c.Close(), Times.Once);
        }

        [Fact]
        public void OutputDirectorySet()
        {
            var outputDirectory = "Output Directory";

            var desktopServiceSettings = new Mock<IDesktopServiceSettings>();

            var viewModel = NewViewModel(desktopServiceSettings.Object);

            viewModel.OutputDirectory = outputDirectory;

            desktopServiceSettings.VerifySet(s => s.OutputDirectory = outputDirectory);
        }

        [Fact]
        public void IlrConnectionStringSet()
        {
            var ilrConnectionString = "Connection String";

            var desktopServiceSettings = new Mock<IDesktopServiceSettings>();

            var viewModel = NewViewModel(desktopServiceSettings.Object);

            viewModel.IlrDatabaseConnectionString = ilrConnectionString;

            desktopServiceSettings.VerifySet(s => s.IlrDatabaseConnectionString = ilrConnectionString);
        }

        private SettingsViewModel NewViewModel(IDesktopServiceSettings desktopServiceSettings = null, IDialogInteractionService dialogInteractionService = null)
        {
            return new SettingsViewModel(desktopServiceSettings ?? Mock.Of<IDesktopServiceSettings>(), dialogInteractionService ?? Mock.Of<IDialogInteractionService>());
        }
    }
}

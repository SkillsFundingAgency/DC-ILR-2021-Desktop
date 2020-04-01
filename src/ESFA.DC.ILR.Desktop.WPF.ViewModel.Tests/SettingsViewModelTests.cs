using System;
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
                .Setup(s => s.OutputDirectory)
                .Returns(currentDirectoryName);

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

            var vm = NewViewModel(desktopServiceSettingsMock.Object);

            vm.OutputDirectory = "Expected";
            vm.IlrDatabaseConnectionString = "DataBase Connection";
            vm.ExportToSql = true;
            vm.ExportToAccessAndCsv = true;

            await vm.SaveSettingsCommand.ExecuteAsync(closeableMock.Object);

            desktopServiceSettingsMock.VerifySet(s => s.OutputDirectory = "Expected");
            desktopServiceSettingsMock.VerifySet(s => s.ExportToSql = true);
            desktopServiceSettingsMock.VerifySet(s => s.IlrDatabaseConnectionString = "DataBase Connection");
            desktopServiceSettingsMock.VerifySet(s => s.ExportToAccessAndCsv = true);

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
        public async Task TestConnectionStringCommand_Success()
        {
            var connectionString = "ConnectionString";

            var cancellationToken = CancellationToken.None;

            var connectivityServiceMock = new Mock<IConnectivityService>();

            connectivityServiceMock.Setup(s => s.SqlServerTestAsync(connectionString, cancellationToken)).ReturnsAsync(true);

            var vm = NewViewModel(connectivityService: connectivityServiceMock.Object);

            vm.IlrDatabaseConnectionString = connectionString;

            await vm.TestConnectionStringCommand.ExecuteAsync();

            vm.ConnectionStringTested.Should().BeTrue();
            vm.ConnectionStringTestInProgress.Should().BeFalse();

            vm.ConnectionStringTestFeedback.Should().Be("Connection String Test Successful");
        }

        [Fact]
        public async Task TestConnectionStringCommand_Fail()
        {
            var connectionString = "ConnectionString";
            var errorMessage = "Error Message";

            var cancellationToken = CancellationToken.None;

            var connectivityServiceMock = new Mock<IConnectivityService>();

            connectivityServiceMock.Setup(s => s.SqlServerTestAsync(connectionString, cancellationToken)).ThrowsAsync(new Exception(errorMessage));

            var vm = NewViewModel(connectivityService: connectivityServiceMock.Object);

            vm.IlrDatabaseConnectionString = connectionString;

            await vm.TestConnectionStringCommand.ExecuteAsync();

            vm.ConnectionStringTested.Should().BeTrue();
            vm.ConnectionStringTestInProgress.Should().BeFalse();

            vm.ConnectionStringTestFeedback.Should().Be(errorMessage);
        }

        [Fact]
        public async Task CloseWindowCommandExecute()
        {
            var ilrDatabaseConnection = "ILRDatabaseConnection";
            var exportToSql = true;
            var outputDirectory = "OutputDirectory";
            var exportToAccessAndCsv = true;

            var closeableMock = new Mock<ICloseable>();

            var desktopServiceSettings = new Mock<IDesktopServiceSettings>();

            desktopServiceSettings.SetupGet(s => s.IlrDatabaseConnectionString).Returns(ilrDatabaseConnection);
            desktopServiceSettings.SetupGet(s => s.ExportToSql).Returns(exportToSql);
            desktopServiceSettings.SetupGet(s => s.OutputDirectory).Returns(outputDirectory);
            desktopServiceSettings.SetupGet(s => s.ExportToAccessAndCsv).Returns(exportToAccessAndCsv);

            var viewmodel = NewViewModel(desktopServiceSettings.Object);

            viewmodel.ExportToSql = false;
            viewmodel.IlrDatabaseConnectionString = "Junk";
            viewmodel.OutputDirectory = "Junk";
            viewmodel.ExportToAccessAndCsv = false;

            viewmodel.CloseWindowCommand.Execute(closeableMock.Object);

            closeableMock.Verify(c => c.Close());

            viewmodel.OutputDirectory.Should().Be(outputDirectory);
            viewmodel.ExportToSql.Should().Be(exportToSql);
            viewmodel.IlrDatabaseConnectionString.Should().Be(ilrDatabaseConnection);
            viewmodel.ExportToAccessAndCsv.Should().Be(exportToAccessAndCsv);
        }

        private SettingsViewModel NewViewModel(IDesktopServiceSettings desktopServiceSettings = null, IDialogInteractionService dialogInteractionService = null, IConnectivityService connectivityService = null)
        {
            return new SettingsViewModel(
                desktopServiceSettings ?? Mock.Of<IDesktopServiceSettings>(),
                dialogInteractionService ?? Mock.Of<IDialogInteractionService>(),
                connectivityService ?? Mock.Of<IConnectivityService>());
        }
    }
}

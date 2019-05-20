using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.WPF.Common;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public void ChooseFileCommandExecute()
        {
            var fileName = "File Name";

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();

            dialogInteractionServiceMock.Setup(s => s.GetFileNameFromOpenFileDialog()).Returns(fileName);

            var viewModel = NewViewModel(dialogInteractionService: dialogInteractionServiceMock.Object);

            viewModel.ChooseFileCommand.Execute(null);

            viewModel.FileName.Should().Be(fileName);
        }

        [Fact]
        public void ChooseFileCommandExecute_Null()
        {
            var fileName = "File Name";

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();

            dialogInteractionServiceMock.Setup(s => s.GetFileNameFromOpenFileDialog()).Returns(null as string);

            var viewModel = NewViewModel(dialogInteractionService: dialogInteractionServiceMock.Object);

            viewModel.FileName = fileName;

            viewModel.ChooseFileCommand.Execute(null);

            viewModel.FileName.Should().Be(fileName);
        }

        [Fact]
        public void ChooseFileCommandExecute_Empty()
        {
            var fileName = "File Name";

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();

            dialogInteractionServiceMock.Setup(s => s.GetFileNameFromOpenFileDialog()).Returns(string.Empty);

            var viewModel = NewViewModel(dialogInteractionService: dialogInteractionServiceMock.Object);

            viewModel.FileName = fileName;

            viewModel.ChooseFileCommand.Execute(null);

            viewModel.FileName.Should().Be(fileName);
        }

        [Fact]
        public void ChooseFileCommandExecute_WhiteSpace()
        {
            var fileName = "File Name";

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();

            dialogInteractionServiceMock.Setup(s => s.GetFileNameFromOpenFileDialog()).Returns("  ");

            var viewModel = NewViewModel(dialogInteractionService: dialogInteractionServiceMock.Object);

            viewModel.FileName = fileName;

            viewModel.ChooseFileCommand.Execute(null);

            viewModel.FileName.Should().Be(fileName);
        }

        [Fact]
        public void SettingsNavigationCommandExecute()
        {
            var windowServiceMock = new Mock<IWindowService>();

            NewViewModel(windowService: windowServiceMock.Object).SettingsNavigationCommand.Execute(null);

            windowServiceMock.Verify(s => s.ShowSettingsWindow(), Times.Once);
        }

        [Fact]
        public void AboutWindowNavigationCommandExecute()
        {
            var windowServiceMock = new Mock<IWindowService>();

            NewViewModel(windowService: windowServiceMock.Object).AboutNavigationCommand.Execute(null);

            windowServiceMock.Verify(s => s.ShowAboutWindow(), Times.Once);
        }

        [Fact]
        public void HandleTaskProgressMessage()
        {
            var taskName = "TaskName";
            var currentTask = 1;
            var taskCount = 2;

            var taskProgressMessage = new TaskProgressMessage(taskName, currentTask, taskCount);

            var viewModel = NewViewModel();

            viewModel.HandleTaskProgressMessage(taskProgressMessage);

            viewModel.TaskName.Should().Be(taskName);
            viewModel.CurrentTask.Should().Be(currentTask);
            viewModel.TaskCount.Should().Be(taskCount);
        }

        [Fact]
        public async Task ProcessFileCommandExecute()
        {
            var fileName = "FileName";
            var cancellationToken = CancellationToken.None;

            var ilrDesktopServiceMock = new Mock<IIlrDesktopService>();

            var viewModel = NewViewModel(ilrDesktopServiceMock.Object);

            ilrDesktopServiceMock.Setup(s => s.ProcessAsync(fileName, cancellationToken))
                .Callback(() => viewModel.Processing.Should().BeTrue())
                .Returns(Task.CompletedTask);

            viewModel.FileName = fileName;

            await viewModel.ProcessFileCommand.ExecuteAsync();

            viewModel.Processing.Should().BeFalse();
        }

        private MainViewModel NewViewModel(IIlrDesktopService ilrDesktopService = null, IMessengerService messengerService = null, IWindowService windowService = null, IDialogInteractionService dialogInteractionService = null)
        {
            return new MainViewModel(
                ilrDesktopService ?? Mock.Of<IIlrDesktopService>(),
                messengerService ?? Mock.Of<IMessengerService>(),
                windowService ?? Mock.Of<IWindowService>(),
                dialogInteractionService ?? Mock.Of<IDialogInteractionService>());
        }
    }
}
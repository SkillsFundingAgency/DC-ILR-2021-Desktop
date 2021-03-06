﻿using ESFA.DC.ILR.Desktop.CLI.Service;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Service.Interface;
using FluentAssertions;
using Xunit;
using ESFA.DC.ILR.Desktop.CLI.Interface;

namespace ESFA.DC.ILR.Desktop.CLI.Tests
{
    public class DesktopContextFactoryTests
    {
        [InlineData("", "settings")]
        [InlineData(null, "settings")]
        [InlineData(" ", "settings")]
        [Theory]
        public void OverrideConfig_Settings(string commandLine, string settings)
        {
            NewFactory().OverrideConfig(commandLine, settings).Should().Be(settings);
        }

        [Fact]
        public void OverrideConfig_CommandLine()
        {
            var commandLine = "Command Line";

            NewFactory().OverrideConfig(commandLine, "Settings").Should().Be(commandLine);
        }

        private DesktopContextFactory NewFactory(IDesktopServiceSettings desktopServiceSettings = null, IDateTimeProvider dateTimeProvider = null, IDesktopSettingsDefaultsService desktopSettingsDefaultsService = null, IAssemblyService assemblyService = null, IReleaseVersionInformationService releaseVersionInformation = null)
        {
            return new DesktopContextFactory(desktopServiceSettings, dateTimeProvider, desktopSettingsDefaultsService, releaseVersionInformation, assemblyService);
        }
    }
}

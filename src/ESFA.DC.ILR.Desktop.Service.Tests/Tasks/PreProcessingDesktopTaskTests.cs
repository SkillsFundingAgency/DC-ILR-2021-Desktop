using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Tasks;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests.Tasks
{
    public class PreProcessingDesktopTaskTests
    {
        [InlineData(null)]
        [InlineData("NonsenseFileName")]
        [InlineData(" ")]
        [InlineData("ILR-1234567A")]
        [InlineData("ILR-1234567")]
        [Theory]
        public void TryGetUkprnFromFileName_False(string fileName)
        {
            NewTask().TryGetUkprnFromFileName(fileName, out var ukprn).Should().BeFalse();

            ukprn.Should().Be(0);
        }

        [Fact]
        public void TryGetUkprnFromFileName_True()
        {
            NewTask().TryGetUkprnFromFileName("ILR-12345678", out var ukprn).Should().BeTrue();

            ukprn.Should().Be(12345678);
        }

        private PreProcessingDesktopTask NewTask()
        {
            return new PreProcessingDesktopTask();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.WPF.ViewModel.ReportFilters;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.Tests
{
    public class ReportFiltersPropertyDefinitionViewModelTests
    {
        [Fact]
        public void IsString_True()
        {
            NewViewModel(typeof(string).FullName).IsString.Should().BeTrue();
        }

        [Fact]
        public void IsString_False()
        {
            NewViewModel(typeof(object).FullName).IsString.Should().BeFalse();
        }

        [Fact]
        public void IsDateTime_True()
        {
            NewViewModel(typeof(DateTime?).FullName).IsDateTime.Should().BeTrue();
        }

        [Fact]
        public void IsDateTime_False()
        {
            NewViewModel(typeof(object).FullName).IsDateTime.Should().BeFalse();
        }

        [Fact]
        public void Value_DateTime()
        {
            DateTime? value = new DateTime(2018, 1, 1);

            var viewModel = NewViewModel(typeof(DateTime?).FullName);

            viewModel.DateTimeValue = value;

            viewModel.Value.Should().Be(value);
        }

        [Fact]
        public void Value_String()
        {
            string value = "test";

            var viewModel = NewViewModel(typeof(string).FullName);

            viewModel.StringValue = value;

            viewModel.Value.Should().Be(value);
        }

        [Fact]
        public void Value_Other()
        {
            var viewModel = NewViewModel(typeof(object).FullName);

            viewModel.Value.Should().BeNull();
        }

        private ReportFiltersPropertyDefinitionViewModel NewViewModel(string type = null)
        {
            return new ReportFiltersPropertyDefinitionViewModel()
            {
                Type = type,
            };
        }
    }
}

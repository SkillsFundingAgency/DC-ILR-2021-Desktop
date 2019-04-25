using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class Tests
    {
        [Fact]
        public void DummyTest()
        {
            true.Should().BeTrue();
        }
    }
}

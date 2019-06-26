using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Context;
using ESFA.DC.ILR.Desktop.Service.Mutator;
using ESFA.DC.ILR.ReportService.Service.Interface;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class SchemaContextMutatorTests
    {
        [Fact]
        public void Mutate_UpdateReportTasks()
        {
            IDesktopContext context = new DesktopContext
            {
                KeyValuePairs = new Dictionary<string, object>
                {
                    { ILRContextKeys.ReportTasks, "TestReport" },
                },
            };

            var mutator = new SchemaErrorContextMutator();

            context = mutator.Mutate(context);

            context.KeyValuePairs[ILRContextKeys.ReportTasks].Should().NotBe("TestReport");
            context.KeyValuePairs[ILRContextKeys.ReportTasks].Should().Be(ReportTaskNameConstants.ValidationSchemaErrorReport);
        }
    }
}

using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Context;
using ESFA.DC.ILR.Desktop.Service.Mutator;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class SchemaContextMutatorTests
    {
        [Fact]
        public void Mutate_UpdateReportTasks()
        {
            IDesktopContext context = new DesktopContext(new DateTime(2018, 1, 1), null, null, "ReferenceData", null, null)
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

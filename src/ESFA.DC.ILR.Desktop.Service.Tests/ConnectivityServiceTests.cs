using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Connectivity;
using FluentAssertions;
using Microsoft.SqlServer.Server;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class ConnectivityServiceTests
    {
        private const string ConnectionString = @"Server=(local);Integrated Security=true;";

        [Fact]
        public void GetMasterConnectionString()
        {
            NewService().GetMasterConnectionString(ConnectionString).Should().Be(@"Data Source=(local);Initial Catalog=Master;Integrated Security=True");
        }

        [Fact]
        public async Task SQLServerTest_Invalid()
        {
            Func<Task> action = async () => await NewService().SqlServerTestAsync("Not A Connection String", CancellationToken.None);

            action.Should().Throw<FormatException>()
                .WithMessage("Your Connection String is not formatted correctly.")
                .WithInnerException<ArgumentException>();
        }

        [Fact]
        public async Task SQLServerTest_ConnectionFailure()
        {
            Func<Task> action = async () => await NewService().SqlServerTestAsync(@"Server=FictionalSqlServer;Integrated Security=true;", CancellationToken.None);

            action.Should().Throw<InvalidOperationException>()
                .WithMessage("Unable to connect to the SQL Server Instance specified, check you have the correct permissions.")
                .WithInnerException<SqlException>();
        }

        [Fact(Skip = "Not Suitable for Build Server")]
        public async Task SQLServerTest_ConnectionSuccess()
        {
            (await NewService().SqlServerTestAsync(ConnectionString, CancellationToken.None)).Should().BeTrue();
        }

        private ConnectivityService NewService()
        {
            return new ConnectivityService();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Connectivity;
using FluentAssertions;
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
        public void SQLServerTest_Invalid()
        {
            Action action = () => NewService().SQLServerTest("Not A Connection String");

            action.Should().Throw<ArgumentException>()
                .WithMessage("Your Connection String is not formatted correctly.")
                .WithInnerException<ArgumentException>();
        }

        [Fact]
        public void SQLServerTest_ConnectionFailure()
        {
            Action action = () => NewService().SQLServerTest(@"Server=FictionalSqlServer;Integrated Security=true;");

            action.Should().Throw<InvalidOperationException>()
                .WithMessage("Unable to connect to the SQL Server Instance specified, check you have the correct permissions.")
                .WithInnerException<SqlException>();
        }

        [Fact(Skip = "Not Suitable for Build Server")]
        public void SQLServerTest_ConnectionSuccess()
        {
            NewService().SQLServerTest(ConnectionString).Should().BeTrue();
        }

        private ConnectivityService NewService()
        {
            return new ConnectivityService();
        }
    }
}

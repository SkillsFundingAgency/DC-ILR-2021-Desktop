using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Connectivity
{
    public class ConnectivityService : IConnectivityService
    {
        private const string SelectStatement = "SELECT 1";

        public bool SQLServerTest(string connectionString)
        {
            string masterConnectionString = null;

            try
            {
                masterConnectionString = GetMasterConnectionString(connectionString);
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException("Your Connection String is not formatted correctly.", exception);
            }

            try
            {
                using (var connection = new SqlConnection(masterConnectionString))
                {
                    var command = new SqlCommand(SelectStatement, connection);

                    connection.Open();
                    command.ExecuteScalar();
                }
            }
            catch (SqlException exception)
            {
                throw new InvalidOperationException("Unable to connect to the SQL Server Instance specified, check you have the correct permissions.", exception);
            }

            return true;
        }

        public string GetMasterConnectionString(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "Master"
            };

            return connectionStringBuilder.ConnectionString;
        }
    }
}

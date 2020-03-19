using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Connectivity
{
    public class ConnectivityService : IConnectivityService
    {
        private const string SelectStatement = "SELECT 1";

        private const string ConnectionStringIncorrectFormatError = "Your Connection String is not formatted correctly.";
        private const string ConnectionFailureError = "Unable to connect to the SQL Server Instance specified, check you have the correct permissions.";

        public async Task<bool> SqlServerTestAsync(string connectionString, CancellationToken cancellationToken)
        {
            string masterConnectionString = null;

            try
            {
                masterConnectionString = GetMasterConnectionString(connectionString);
            }
            catch (Exception exception)
            {
                throw new FormatException(ConnectionStringIncorrectFormatError, exception);
            }

            try
            {
                using (var connection = new SqlConnection(masterConnectionString))
                {
                    var command = new SqlCommand(SelectStatement, connection);

                    await connection.OpenAsync(cancellationToken);
                    await command.ExecuteScalarAsync(cancellationToken);
                }
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(ConnectionFailureError, exception);
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

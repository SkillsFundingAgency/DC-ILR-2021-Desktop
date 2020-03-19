using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IConnectivityService
    {
        Task<bool> SqlServerTestAsync(string connectionString, CancellationToken cancellationToken);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IConnectivityService
    {
        bool SQLServerTest(string connectionString);
    }
}

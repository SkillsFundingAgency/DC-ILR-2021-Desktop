using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class DesktopServiceSettingsStub : IDesktopServiceSettings
    {
        public string IlrDatabaseConnectionString { get; set; }

        public string OutputDirectory { get; set; }
    }
}

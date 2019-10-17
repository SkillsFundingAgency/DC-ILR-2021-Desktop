using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.WPF.Config;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class ReferenceDataVersionInformationService : IReferenceDataVersionInformationService
    {
        public string Date => DesktopServiceConfiguration.Configuration.ReferenceDataDate;

        public string VersionNumber => "123.123.123";
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Tasks.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public class PreProcessingDesktopTaskContext : IPreProcessingDesktopTaskContext
    {
        private readonly IDesktopContext _desktopContext;

        public PreProcessingDesktopTaskContext(IDesktopContext desktopContext)
        {
            _desktopContext = desktopContext;
        }

        public string FileName
        {
            get => _desktopContext.KeyValuePairs[ILRContextKeys.Filename].ToString();
            set => _desktopContext.KeyValuePairs[ILRContextKeys.Filename] = value;
        }

        public string OriginalFileName
        {
            get => _desktopContext.KeyValuePairs[ILRContextKeys.OriginalFilename].ToString();
            set => _desktopContext.KeyValuePairs[ILRContextKeys.OriginalFilename] = value;
        }

        public string Container
        {
            get => _desktopContext.KeyValuePairs[ILRContextKeys.Container].ToString();
            set => _desktopContext.KeyValuePairs[ILRContextKeys.Container] = value;
        }
    }
}

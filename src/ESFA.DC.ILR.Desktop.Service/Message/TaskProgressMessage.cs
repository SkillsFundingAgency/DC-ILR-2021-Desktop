using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.Desktop.Service.Message
{
    public class TaskProgressMessage
    {
        public TaskProgressMessage(string taskName)
        {
            TaskName = taskName;
        }

        public string TaskName { get; }
    }
}

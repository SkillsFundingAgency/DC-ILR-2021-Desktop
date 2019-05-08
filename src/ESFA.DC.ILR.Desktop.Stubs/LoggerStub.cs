using System;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class LoggerStub : ILogger
    {
        public void Dispose()
        {
        }

        public void LogFatal(string message, Exception exception = null, object[] parameters = null, long jobIdOverride = -1, string callerMemberName = "", string callerFilePath = "", int callerLineNumber = 0)
        {
        }

        public void LogError(string message, Exception exception = null, object[] parameters = null, long jobIdOverride = -1, string callerMemberName = "", string callerFilePath = "", int callerLineNumber = 0)
        {
        }

        public void LogWarning(string message, object[] parameters = null, long jobIdOverride = -1, string callerMemberName = "", string callerFilePath = "", int callerLineNumber = 0)
        {
        }

        public void LogDebug(string message, object[] parameters = null, long jobIdOverride = -1, string callerMemberName = "", string callerFilePath = "", int callerLineNumber = 0)
        {
        }

        public void LogInfo(string message, object[] parameters = null, long jobIdOverride = -1, string callerMemberName = "", string callerFilePath = "", int callerLineNumber = 0)
        {
        }

        public void LogVerbose(string message, object[] parameters = null, long jobIdOverride = -1, string callerMemberName = "", string callerFilePath = "", int callerLineNumber = 0)
        {
        }
    }
}

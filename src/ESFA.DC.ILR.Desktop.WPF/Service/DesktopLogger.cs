using System;
using System.Runtime.CompilerServices;
using NLog;
using NLog.Fluent;
using ILogger = ESFA.DC.Logging.Interfaces.ILogger;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class DesktopLogger : Logger, ILogger
    {
        public void Dispose()
        {
        }

        public void LogFatal(string message, Exception exception = null, object[] parameters = null, long jobIdOverride = -1, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var logEventInfo = this
                .Log(LogLevel.Fatal)
                .Message(message)
                .Exception(exception)
                .LogEventInfo;

            logEventInfo.SetCallerInfo(string.Empty, callerMemberName, callerFilePath, callerLineNumber);

            this.Log(logEventInfo);
        }

        public void LogError(string message, Exception exception = null, object[] parameters = null, long jobIdOverride = -1, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var logEventInfo = this
                .Log(LogLevel.Error)
                .Message(message)
                .Exception(exception)
                .LogEventInfo;

            logEventInfo.SetCallerInfo(string.Empty, callerMemberName, callerFilePath, callerLineNumber);

            this.Log(logEventInfo);
        }

        public void LogWarning(string message, object[] parameters = null, long jobIdOverride = -1, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var logEventInfo = this
                .Log(LogLevel.Warn)
                .Message(message)
                .LogEventInfo;

            logEventInfo.SetCallerInfo(string.Empty, callerMemberName, callerFilePath, callerLineNumber);

            this.Log(logEventInfo);
        }

        public void LogDebug(string message, object[] parameters = null, long jobIdOverride = -1, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var logEventInfo = this
                .Log(LogLevel.Debug)
                .Message(message)
                .LogEventInfo;

            logEventInfo.SetCallerInfo(string.Empty, callerMemberName, callerFilePath, callerLineNumber);

            this.Log(logEventInfo);
        }

        public void LogInfo(string message, object[] parameters = null, long jobIdOverride = -1, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var logEventInfo = this
                .Log(LogLevel.Info)
                .Message(message)
                .LogEventInfo;

            logEventInfo.SetCallerInfo(string.Empty, callerMemberName, callerFilePath, callerLineNumber);

            this.Log(logEventInfo);
        }

        public void LogVerbose(string message, object[] parameters = null, long jobIdOverride = -1, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var logEventInfo = this
                .Log(LogLevel.Trace)
                .Message(message)
                .LogEventInfo;

            logEventInfo.SetCallerInfo(string.Empty, callerMemberName, callerFilePath, callerLineNumber);

            this.Log(logEventInfo);
        }
    }
}

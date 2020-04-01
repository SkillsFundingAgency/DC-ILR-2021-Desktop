using System;
using System.IO;
using System.Net.Http;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.ILR.Desktop.Utils.Polly.Interface;
using Polly;
using Polly.Retry;

namespace ESFA.DC.ILR.Desktop.Utils.Polly
{
    public class PollyPolicies : IPollyPolicies
    {
        private readonly ILogger _logger;

        public PollyPolicies(ILogger logger)
        {
            _logger = logger;
            FileSystemRetryPolicy = FileSystemRetry();
            RequestTimeoutAsyncRetryPolicy = RequestTimeoutAsyncRetry();
        }

        public RetryPolicy FileSystemRetryPolicy { get; }

        public AsyncRetryPolicy RequestTimeoutAsyncRetryPolicy { get; }

        private RetryPolicy FileSystemRetry() => 
            Policy
             .Handle<IOException>()
             .Or<DirectoryNotFoundException>()
             .WaitAndRetry(
                 new TimeSpan[]
                 {
                        TimeSpan.FromMilliseconds(500),
                        TimeSpan.FromSeconds(1000),
                        TimeSpan.FromSeconds(2000),
                 },
                 (exception, span) =>
                     _logger.LogError($"Exception Caught, retry in {span.Milliseconds} Milliseconds", exception));

        private AsyncRetryPolicy RequestTimeoutAsyncRetry() =>
           Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                3, // number of retries
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // exponential backoff
                (exception, timeSpan, retryCount, executionContext) =>
                {
                    _logger.LogError("Request exceeded max retries", exception);
                });
    }
}

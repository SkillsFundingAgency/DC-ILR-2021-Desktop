using System.Net.Http;
using Polly.Retry;

namespace ESFA.DC.ILR.Desktop.Utils.Polly.Interface
{
    public interface IPollyPolicies
    {
        RetryPolicy FileSystemRetryPolicy { get; }

        AsyncRetryPolicy RequestTimeoutAsyncRetryPolicy { get; }
    }
}

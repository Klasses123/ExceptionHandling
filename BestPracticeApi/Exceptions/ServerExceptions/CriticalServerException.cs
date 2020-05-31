using System;

namespace BestPracticeApi.Exceptions.ServerExceptions
{
    public sealed class CriticalServerException : Exception
    {
        public string Description { get; }
        public object Details { get; }
        public Exception OriginalException { get; }
        public CriticalServerException(string desc, Exception originalException, object details = null)
        {
            Description = desc;
            OriginalException = originalException;
            Details = details;
        }
    }
}

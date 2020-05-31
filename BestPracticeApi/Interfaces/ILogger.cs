using System;
using System.Collections.Generic;

namespace BestPracticeApi.Interfaces
{
    public interface ILogger
    {
        void Fatal(object message, Exception ex);
        void Error(Exception exception);
        void Error(string description, Exception exception);
        void Error(string message);
        void Error(IEnumerable<Exception> exceptions);
        void Information(string message);
    }
}

using System;

namespace BestPracticeApi.Exceptions.ClientExceptions
{
    public class NotExistsException : Exception
    {
        public NotExistsException(string message) : base(message) { }
    }
}

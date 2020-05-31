using System;

namespace BestPracticeApi.Exceptions.ClientExceptions
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException(string message) : base(message) { }
    }
}

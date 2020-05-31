using System;
using System.Collections.Generic;

namespace BestPracticeApi.Exceptions.ClientExceptions
{
    public class MissingParametersException : Exception
    {
        public IList<string> MissingParameters { get; set; }

        public MissingParametersException(IList<string> missingParams)
        {
            MissingParameters = missingParams;
        }
    }
}

using System;

namespace BestPracticeApi.Exceptions.ClientExceptions
{
    public class AlreadyExistException : Exception
    {
		public string ColumnName { get; }
		public object Value { get; }

		public AlreadyExistException(string columnName, object value)
		{
			ColumnName = columnName;
			Value = value;
		}
	}
}

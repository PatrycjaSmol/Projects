using System;

namespace ToDoList.Common.Exceptions
{
    public class DataLimitException : Exception
    {
        public DataLimitException()
        {
        }

        public DataLimitException(string message)
            : base(message)
        {
        }
    }
}

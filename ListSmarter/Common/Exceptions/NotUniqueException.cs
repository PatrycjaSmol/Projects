using System;

namespace ToDoList.Common.Exceptions
{
    public class NotUniqueException : Exception
    {
        public NotUniqueException()
        {
        }

        public NotUniqueException(string message)
            : base(message)
        {
        }
    }
}

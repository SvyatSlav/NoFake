using System;

namespace NoFake.IO.Exceptions
{
    class DirectoryTakeException : Exception
    {
        public DirectoryTakeException()
        {
        }

        public DirectoryTakeException(string message) : base(message)
        {
        }

        public DirectoryTakeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

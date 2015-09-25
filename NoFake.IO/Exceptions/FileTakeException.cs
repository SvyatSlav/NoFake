using System;

namespace NoFake.IO.Exceptions
{
    class FileTakeException : Exception
    {
        public FileTakeException(string message) : base(message)
        {
        }

        public FileTakeException()
        {
        }

        public FileTakeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

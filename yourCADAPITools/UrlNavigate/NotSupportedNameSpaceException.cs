using System;
using System.Runtime.Serialization;

namespace yourCADAPITools
{
    [Serializable]
    internal class NotSupportedNameSpaceException : Exception
    {
        public NotSupportedNameSpaceException()
        {
        }

        public NotSupportedNameSpaceException(string message) : base(message)
        {
        }

        public NotSupportedNameSpaceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSupportedNameSpaceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
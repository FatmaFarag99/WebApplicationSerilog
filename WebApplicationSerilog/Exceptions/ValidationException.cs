namespace WebApplicationSerilog.Exceptions
{
    using System.Runtime.Serialization;
 
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException() : base() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
        public ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
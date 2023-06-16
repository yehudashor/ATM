using System.Runtime.Serialization;

namespace TransactionService.Exceptions;

[Serializable]
public class BadRequestException : Exception
{
    private object message;

    public BadRequestException()
    {
    }

    public BadRequestException(object message)
    {
        this.message = message;
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
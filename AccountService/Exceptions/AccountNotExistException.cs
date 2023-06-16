using System.Runtime.Serialization;

namespace AccountService.Exceptions;

[Serializable]
internal class AccountNotExistException : Exception
{
    public AccountNotExistException()
    {
    }

    public AccountNotExistException(string message) : base(message)
    {
    }

    public AccountNotExistException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected AccountNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
using System.Runtime.Serialization;

namespace TransactionService.Exceptions;

[Serializable]
internal class BillsDistributionException : BadRequestException
{
    public BillsDistributionException()
    {
    }

    public BillsDistributionException(string message) : base(message)
    {
    }

    public BillsDistributionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BillsDistributionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
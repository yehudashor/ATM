namespace TransactionService.Exceptions;

public class TransactionProcessingException : BadRequestException
{
    public TransactionProcessingException() : base("Error occurred during transaction processing.")
    {
    }
}

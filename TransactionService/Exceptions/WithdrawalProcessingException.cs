namespace TransactionService.Exceptions;

public class WithdrawalProcessingException : BadRequestException
{
    public WithdrawalProcessingException() : base("Error occurred during withdrawal processing.")
    {
    }
}

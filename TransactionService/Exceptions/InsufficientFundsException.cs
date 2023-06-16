namespace TransactionService.Exceptions;
public class InsufficientFundsException : BadRequestException
{
    public InsufficientFundsException() : base("Insufficient funds in the account.")
    {
    }
}
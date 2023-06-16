using static TransactionService.Commands.Implementations.DepositAccountCommand;

namespace TransactionService.ATMOperations.Deposit;

public interface IATMDeposit
{
    Task Deposit(DepositCommand depositCommand);
}
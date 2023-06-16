using static TransactionService.Commands.Implementations.DepositAccountCommand;

namespace TransactionService.Commands.Interfaces;

public interface IDepositAccountCommand
{
    Task AccountDeposit(DepositCommand depositCommand);
}

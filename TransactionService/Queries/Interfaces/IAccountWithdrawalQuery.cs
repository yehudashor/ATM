using static TransactionService.Queries.Implementations.AccountWithdrawalQuery;

namespace TransactionService.Queries.Interfaces;

public interface IAccountWithdrawalQuery
{
    Task AccountWithdrawal(WithdrawalQuery withdrawalQuery);
}

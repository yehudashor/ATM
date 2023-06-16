using TransactionDataModel.Models.Entities.Enums;
using static TransactionService.Queries.Implementations.AccountWithdrawalQuery;

namespace TransactionService.ATMOperations.Withdrawal;

public interface IATMWithdrawal
{
    Task InitBillsForWithdrawal(WithdrawalQuery withdrawalCommand);

    Task<Dictionary<BillType, long>> Withdrawal();
}


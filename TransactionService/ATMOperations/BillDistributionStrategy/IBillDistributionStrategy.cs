using TransactionDataModel.Models.Entities.Enums;

namespace TransactionService.ATMOperations.BillDistributionStrategy;

public interface IBillDistributionStrategy
{
    Dictionary<BillType, long> GetDistributeBills(long amount, Func<BillType, long, long> func = null);
}



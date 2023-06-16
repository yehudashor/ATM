using TransactionDataModel.Models.Entities.Bill;
using TransactionDataModel.Models.Entities.Enums;

namespace TransactionDataModel.DataSource;

internal class BillsDataSource
{
    private const int _startBillesCount = 4;

    /// <summary>
    /// Generates a random collection of bills.
    /// </summary>
    /// <returns></returns>
    internal static IEnumerable<Bill> GetBills()
    {
        var billsTypes = Enum.GetValues<BillType>().Cast<BillType>();

        return from billType in billsTypes
               select new Bill
               {
                   BillType = billType,
                   Amount = _startBillesCount
               };
    }
}

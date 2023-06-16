using TransactionDataModel.Models.Entities.Enums;

namespace TransactionDataModel.Models.Entities.Bill;

public class Bill
{
    public BillType BillType { get; set; }

    public long Amount { get; set; }

    public long Total => (int)BillType * Amount;
}

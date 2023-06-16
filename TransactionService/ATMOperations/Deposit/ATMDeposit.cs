using Microsoft.EntityFrameworkCore;
using TransactionDataModel.DatabaseContext;
using TransactionDataModel.Models.Entities.Bill;
using TransactionDataModel.Models.Entities.Enums;
using TransactionService.ATMOperations.BillDistributionStrategy;
using static TransactionService.Commands.Implementations.DepositAccountCommand;

namespace TransactionService.ATMOperations.Deposit;

public class ATMDeposit : IATMDeposit
{
    private readonly IBillDistributionStrategy _billDistributionStrategy;

    private readonly BillDbContext _billContext;

    public ATMDeposit(IBillDistributionStrategy billDistributionStrategy, BillDbContext billContext)
    {
        _billDistributionStrategy = billDistributionStrategy;
        this._billContext = billContext;
    }

    public async Task Deposit(DepositCommand depositCommand)
    {

        var bills = await _billContext.Bills.ToListAsync();
        var depositBills = _billDistributionStrategy.GetDistributeBills(depositCommand.Amount);

        await InitBillsForDeposit(bills, depositBills);
    }

    private async Task InitBillsForDeposit(List<Bill> bills, Dictionary<BillType, long> depositBills)
    {
        foreach (var bill in bills)
        {
            if (depositBills.TryGetValue(bill.BillType, out long count))
            {
                bill.Amount += count;
            }
        }
        await _billContext.SaveChangesAsync();
    }
}

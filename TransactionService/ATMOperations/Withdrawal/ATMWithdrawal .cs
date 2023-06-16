using Microsoft.EntityFrameworkCore;
using TransactionDataModel.DatabaseContext;
using TransactionDataModel.Models.Entities.Enums;
using TransactionService.ATMOperations.BillDistributionStrategy;
using static TransactionService.Queries.Implementations.AccountWithdrawalQuery;

namespace TransactionService.ATMOperations.Withdrawal
{
    /// <summary>
    /// Represents the ATM withdrawal operation.
    /// </summary>
    public class ATMWithdrawal : IATMWithdrawal
    {
        private readonly IBillDistributionStrategy _billDistributionStrategy;

        private readonly BillDbContext _billContext;

        private Dictionary<BillType, long> _withdrawalBills;

        public ATMWithdrawal(IBillDistributionStrategy billDistributionStrategy, BillDbContext billContext)
        {
            _billDistributionStrategy = billDistributionStrategy;
            _billContext = billContext;
        }

        /// <summary>
        /// Initializes the bills for withdrawal based on the specified withdrawal command.
        /// </summary>
        /// <param name="withdrawalCommand">The withdrawal command.</param>
        public async Task InitBillsForWithdrawal(WithdrawalQuery withdrawalCommand)
        {
            var bills = await _billContext.Bills.ToListAsync();

            // Create a dictionary of bill types and their available amounts
            var countBillsType = bills.ToDictionary(b => b.BillType, b => b.Amount);

            // Define a function to get the minimal count between the available bills and the desired count
            Func<BillType, long, long> minimalBetweenCount =
                (billType, billCount) => Math.Min(countBillsType[billType], billCount);

            // Use the bill distribution strategy to get the bills for withdrawal
            _withdrawalBills = _billDistributionStrategy.GetDistributeBills(withdrawalCommand.Amount, minimalBetweenCount);
        }

        /// <summary>
        /// Performs the withdrawal operation and updates the bill amounts in the database.
        /// </summary>
        /// <returns>A dictionary containing the withdrawn bills.</returns>
        public async Task<Dictionary<BillType, long>> Withdrawal()
        {
            var bills = await _billContext.Bills.ToListAsync();

            foreach (var bill in bills)
            {
                if (_withdrawalBills.TryGetValue(bill.BillType, out long count))
                {
                    // Subtract the withdrawn count from the bill's amount
                    bill.Amount -= count;
                }
            }

            await _billContext.SaveChangesAsync();

            // Return the withdrawn bills
            return _withdrawalBills;
        }
    }
}

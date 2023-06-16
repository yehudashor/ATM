using TransactionDataModel.Models.Entities.Enums;
using TransactionService.Exceptions;

namespace TransactionService.ATMOperations.BillDistributionStrategy
{
    /// <summary>
    /// Implements a high distribution strategy for bills distribution.
    /// </summary>
    public class HighDistributionStrategy : IBillDistributionStrategy
    {
        /// <summary>
        /// Distributes bills based on the specified amount using a high distribution strategy.
        /// </summary>
        /// <param name="amount">The amount to distribute.</param>
        /// <param name="func">An optional function in case there is a limit on the
        /// amount of bills that exist of a certain type (for example in a withdrawal)</param>
        /// <returns>A dictionary containing the distributed bills.</returns>
        public Dictionary<BillType, long> GetDistributeBills(long amount, Func<BillType, long, long> func = null)
        {
            // Subtracts 20 bills from the amount until it is divisible by 50.
            long sumOfTwentyBills = subtractingTwentyBills(amount);

            if (func is not null && func(BillType.Twenty, sumOfTwentyBills) != sumOfTwentyBills)
            {
                throw new BillsDistributionException();
            }

            var bills = new Dictionary<BillType, long>
            {
                { BillType.Twenty, sumOfTwentyBills }
            };

            amount -= sumOfTwentyBills * (int)BillType.Twenty;

            helpGetDistributedBills(ref amount, bills, func!);

            // If the amount is still not zero, it means it was not possible
            if (amount is not 0)
            {
                throw new BillsDistributionException();
            }

            return bills;
        }

        /// <summary>
        /// Helper method to distribute bills based on the remaining amount and bill types.
        /// </summary>
        /// <param name="amount">The remaining amount to distribute.</param>
        /// <param name="bills">The dictionary to store the distributed bills.</param>
        /// <param name="func">The function to adjust the bill amount for a specific bill type.</param>
        private void helpGetDistributedBills(ref long amount, Dictionary<BillType, long> bills, Func<BillType, long, long> func)
        {
            // Get all the bill types in descending order
            IEnumerable<BillType> billTypes = Enum.GetValues(typeof(BillType))
                .Cast<BillType>()
                .OrderByDescending(b => b);

            // Iterate through each bill type
            foreach (var billType in billTypes)
            {
                // If the amount has reached zero, break the loop
                if (amount is 0)
                {
                    break;
                }

                var billSum = (int)billType;

                // Calculate the number of bills of the current type
                long billAmount = amount / billSum;

                // Check against the provided function (if available)
                // to get the adjusted bill amount
                if (func is not null)
                {
                    billAmount = func(billType, billAmount);
                }

                // Reduce the amount by the number of bills multiplied by the bill value
                amount -= billAmount * billSum;

                // Add the bill type and count to the dictionary
                bills.Add(billType, billAmount);
            }
        }

        /// <summary>
        /// Subtracts 20 bills from the amount until it is divisible by 50.
        /// </summary>
        /// <param name="amount">The amount to subtract from.</param>
        /// <returns>The number of subtracted 20 bills.</returns>
        private int subtractingTwentyBills(long amount)
        {
            int counter = 0;

            // Subtract 20 bills until the amount is divisible by 50
            while (amount % 50 != 0 && amount >= 20)
            {
                amount -= 20;
                counter++;
            }

            return counter;
        }
    }
}

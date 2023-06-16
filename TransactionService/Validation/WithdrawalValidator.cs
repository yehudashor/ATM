using FluentValidation;
using TransactionDataModel.DatabaseContext;
using TransactionDataModel.Models.Entities.Bill;
using TransactionService.Extensions;
using static TransactionService.Queries.Implementations.AccountWithdrawalQuery;

namespace TransactionService.Validation;

public class WithdrawalValidator : AbstractValidator<WithdrawalQuery>
{
    public WithdrawalValidator()
    {
        // AccountId > 0.
        RuleFor(v => v.AccountId)
            .GreaterThan(0)
            .WithMessage(Constants.Constants.AccountIdErrorMessage);

        // AccountId length is 6.
        RuleFor(v => v.AccountId.ToString().Length).Equal(6)
          .WithMessage(Constants.Constants.AccountIdWrongLengthMessage);

        // Amount length is valid withdrawal amount.
        RuleFor(v => v.Amount)
            .Must(amount => amount.IsValidDepositWithdrawalAmount())
            .WithMessage(Constants.Constants.AmountErrorMessage);

        // Check can withdraw.
        using (BillDbContext billContext = new BillDbContext())
        {
            var bills = billContext.Bills.ToList();
            RuleFor(v => v.Amount).Must(amount => canWithdraw(amount, bills))
            .WithMessage(Constants.Constants.WithdrawalErrorMessage);
        }
    }

    /// <summary>
    /// Check if can Withdraw.
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="bills"></param>
    /// <returns>if the amount to withdraw >= sum of all bills.</returns>
    private bool canWithdraw(long amount, List<Bill> bills)
    {
        // if the amount to withdraw >= sum of all bills.
        long totalAmount = bills.Sum(b => b.Amount * (int)b.BillType);
        return totalAmount >= amount;
    }
}

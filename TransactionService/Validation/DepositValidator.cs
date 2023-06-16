using FluentValidation;
using TransactionService.Extensions;
using static TransactionService.Commands.Implementations.DepositAccountCommand;

namespace TransactionService.Validation;

public class DepositValidator : AbstractValidator<DepositCommand>
{
    public DepositValidator()
    {
        RuleFor(v => v.AccountId).GreaterThan(0)
            .WithMessage(Constants.Constants.AccountIdErrorMessage);

        RuleFor(v => v.AccountId.ToString().Length).Equal(6)
          .WithMessage(Constants.Constants.AccountIdWrongLengthMessage);

        RuleFor(v => v.Amount)
            .Must(amount => amount.IsValidDepositWithdrawalAmount())
            .WithMessage(Constants.Constants.AmountErrorMessage);
    }
}

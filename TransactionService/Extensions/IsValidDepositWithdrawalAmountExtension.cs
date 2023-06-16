namespace TransactionService.Extensions;

public static class IsValidDepositWithdrawalAmountExtension
{
    public static bool IsValidDepositWithdrawalAmount(this long amount)
    => amount is not 10 and not 30 and > 0 && amount % 10 == 0;
}

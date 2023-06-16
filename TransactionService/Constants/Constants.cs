namespace TransactionService.Constants;

public class Constants
{
    public const string AccountIdErrorMessage = "Account ID must be greater than zero.";

    public const string AccountIdWrongLengthMessage = "Invalid Account ID. Account ID must be exactly 6 characters long.";

    public const string AmountErrorMessage = "Invalid amount for deposit/withdrawal. " +
        "Please select an amount that is divisible by 20 and 50 (e.g., 20, 40, 50, 60, 70, etc.).";

    public const string WithdrawalErrorMessage = "Insufficient balance to withdraw the specified amount.";

    public const string getAccountUrl = "https://localhost:7091/Account/GetAccount/accountId/";

    public const string updateAccountUrl = "https://localhost:7091/Account/UpdateAccount";
}

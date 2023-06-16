using AccountDataModel.Models.Entities.Account;

namespace AccountDataModel.DataSource;

internal class AccountsDataSource
{
    private static Random _random = new Random(DateTime.Now.Millisecond);

    /// <summary>
    ///  Generates a random collection of accounts
    /// </summary>
    /// <returns></returns>
    internal static IEnumerable<Account> GetAccounts()
    {
        for (int i = 0; i < _random.Next(5, 10); i++)
        {
            var accountId = _random.Next(100000, 1000000);

            var balance = _random.Next(100, 500000);
            yield return new Account { AccountId = accountId, Balance = balance };
        }
    }
}

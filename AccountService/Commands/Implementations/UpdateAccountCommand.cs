using AccountDataModel.DatabaseContext;
using AccountService.Commands.Interfaces;
using AccountService.Exceptions;
using MediatR;

namespace AccountService.Commands.Implementations;

public class UpdateAccountCommand : IUpdateAccountCommand
{
    private readonly AccountDbContext _accountContext;

    public UpdateAccountCommand(AccountDbContext dbContext) =>
        _accountContext = dbContext;

    public async Task UpdateAccount(AccountCommand accountCommand)
    {
        var account = _accountContext.Accounts.FirstOrDefault
            (account => account.AccountId == accountCommand.AccountId);

        if (account is null)
        {
            throw new AccountNotExistException(Constants.Constants.AccountNotExistException);
        }

        account.Balance += accountCommand.Amount;
        await _accountContext.SaveChangesAsync();
    }

    public class AccountCommand : IRequest
    {
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
}

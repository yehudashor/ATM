using AccountService.Commands.Interfaces;
using MediatR;
using static AccountService.Commands.Implementations.UpdateAccountCommand;

namespace AccountService.Handlers;

internal class UpdateAccountBalanceHandler : IRequestHandler<AccountCommand>
{
    private readonly IUpdateAccountCommand _updateAccountCommand;

    public UpdateAccountBalanceHandler(IUpdateAccountCommand updateAccountCommand)
        => _updateAccountCommand = updateAccountCommand;

    /// <summary>
    /// Update account balance: Given an account number and an amount (positive or negative),
    /// the function update the account balance accordingly.
    /// </summary>
    /// <param name="accountCommand">account to update</param>
    /// <returns></returns>
    /// <exception cref="AccountNotExistException"></exception>
    public async Task Handle(AccountCommand accountCommand, CancellationToken cancellationToken)
    {
        await _updateAccountCommand.UpdateAccount(accountCommand);
    }
}

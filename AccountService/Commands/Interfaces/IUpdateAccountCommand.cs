using static AccountService.Commands.Implementations.UpdateAccountCommand;

namespace AccountService.Commands.Interfaces;

internal interface IUpdateAccountCommand
{
    Task UpdateAccount(AccountCommand accountCommand);
}

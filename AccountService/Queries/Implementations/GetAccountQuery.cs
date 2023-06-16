using AccountDataModel.DatabaseContext;
using AccountDataModel.Models.Entities.Account;
using AccountService.Exceptions;
using AccountService.Queries.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static AccountService.Handlers.GetAccountBalanceHandler;

namespace AccountService.Queries.Implementations;

internal class GetAccountQuery : IGetAccountQuery
{
    private readonly AccountDbContext _accountContext;

    public GetAccountQuery(AccountDbContext dbContext) =>
        _accountContext = dbContext;

    public async Task<Account> GetAccount(Expression<Func<Account, bool>> filter)
    {
        var account = await _accountContext.Accounts.FirstOrDefaultAsync(filter);
        return account! ?? throw new AccountNotExistException(Constants.Constants.AccountNotExistException);
    }

    public class AccountQuery : IRequest<AccountQueryResult>
    {
        public int AccountId { get; set; }
    }
}
using AccountDataModel.Models.Entities.Account;
using System.Linq.Expressions;

namespace AccountService.Queries.Interfaces;

internal interface IGetAccountQuery
{
    Task<Account> GetAccount(Expression<Func<Account, bool>> filter);
}

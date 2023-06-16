using AccountService.Queries.Interfaces;
using AutoMapper;
using MediatR;
using static AccountService.Queries.Implementations.GetAccountQuery;

namespace AccountService.Handlers;

internal class GetAccountBalanceHandler : IRequestHandler<AccountQuery, GetAccountBalanceHandler.AccountQueryResult>
{
    private readonly IMapper _mapper;
    private readonly IGetAccountQuery _getAccountQuery;

    public GetAccountBalanceHandler(IMapper mapper, IGetAccountQuery getAccountQuery)
        => (_mapper, _getAccountQuery) = (mapper, getAccountQuery);

    /// <summary>
    /// Get account balance: Given an account number (AccountQuery),
    /// retrieve and return the account balance.
    /// </summary>
    /// <param name="accountQuery"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<AccountQueryResult> Handle(AccountQuery accountQuery, CancellationToken cancellationToken)
    {
        var account = await _getAccountQuery.GetAccount(account =>
                                           account.AccountId == accountQuery.AccountId);

        var result = _mapper.Map<AccountQueryResult>(account);
        return result;
    }

    internal class AccountQueryResult
    {
        public decimal Balance { init; get; }
    }
}

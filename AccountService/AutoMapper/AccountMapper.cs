using AccountDataModel.Models.Entities.Account;
using AutoMapper;
using static AccountService.Handlers.GetAccountBalanceHandler;

namespace AccountService.AutoMapper;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<Account, AccountQueryResult>().ReverseMap();
    }
}

using AccountDataModel.DataSource;
using AccountDataModel.Models.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountDataModel.Models.EntityConfigurations;

/// <summary>
/// Entity type configuration for account.
/// </summary>
internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.AccountId);
        builder.HasIndex(a => a.Balance);
        builder.HasData(AccountsDataSource.GetAccounts());
    }
}

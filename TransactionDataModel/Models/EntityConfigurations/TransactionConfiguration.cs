using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionDataModel.DataSource;
using TransactionDataModel.Models.Entities.Bill;

namespace TransactionDataModel.Models.EntityConfigurations;

internal class TransactionConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.HasKey(b => b.BillType);
        builder.HasIndex(b => b.Amount);
        builder.HasData(BillsDataSource.GetBills());
    }
}

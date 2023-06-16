using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TransactionDataModel.Models.Entities.Bill;

namespace TransactionDataModel.DatabaseContext;

public class BillDbContext : DbContext
{
    public DbSet<Bill> Bills { set; get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStringKeys.ConnectionStringKeys.connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var courrentAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(courrentAssembly);
    }
}

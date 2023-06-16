using AccountDataModel.Models.Entities.Account;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AccountDataModel.DatabaseContext;

public class AccountDbContext : DbContext
{
    public DbSet<Account> Accounts { set; get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStringKeys.ConnectionStringKeys.connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(currentAssembly);
    }
}

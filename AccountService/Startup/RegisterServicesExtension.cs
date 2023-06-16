using AccountDataModel.DatabaseContext;
using AccountService.AutoMapper;
using ATMCore.RegisterServicesExtensions;
using System.Reflection;

namespace AccountService.Startup;

internal static class RegisterServicesExtension
{
    internal static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AccountDbContext>();
        builder.Services.RegisterAutoMapper<AccountMapper>();
        builder.Services.AddScoped<AccountOperations.AccountOperations>();
        builder.Services.RegisterRabbitM();

        var currentAssembly = Assembly.GetExecutingAssembly();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(currentAssembly));
        builder.Host.RegisterHostBuilder(currentAssembly.FullName!, t => t.GetInterface("I" + t.Name) is not null && t.IsClass);

        return builder;
    }
}

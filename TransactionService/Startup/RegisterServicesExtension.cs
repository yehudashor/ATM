using ATMCore.RegisterServicesExtensions;
using FluentValidation.AspNetCore;
using System.Reflection;
using TransactionDataModel.DatabaseContext;
using TransactionService.ATMOperations.BillDistributionStrategy;

namespace TransactionService.Startup;

internal static class RegisterServicesExtension
{
    [Obsolete]
    internal static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddRouting();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<BillDbContext>();
        builder.Services.RegisterRabbitM();
        builder.Services.AddSingleton<IBillDistributionStrategy, HighDistributionStrategy>();

        var currentAssembly = Assembly.GetExecutingAssembly();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(currentAssembly));

        builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        builder.Services.AddFluentValidation(options =>
                 {
                     options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                 });

        builder.Host.RegisterHostBuilder(currentAssembly.FullName!, t => t.GetInterface("I" + t.Name) is not null && t.IsClass);

        return builder;
    }
}

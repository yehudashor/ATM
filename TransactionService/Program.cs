// Add services to the container.
// Configure the HTTP request pipeline.

using TransactionService.Startup;

// Add services to the container.
await WebApplication.CreateBuilder(args)
    .RegisterServices()
    .Build()
    .RegisterMiddlewares()
    .RunAsync();


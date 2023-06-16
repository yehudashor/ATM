using AccountService.Startup;

// Add services to the container.
// Configure the HTTP request pipeline.

var webBuilder = WebApplication.CreateBuilder(args)
    .RegisterServices()
    .Build();

//var accountOperations = webBuilder.Services.GetService<AccountOperations>();


await webBuilder.RegisterMiddlewares()
    .RunAsync();




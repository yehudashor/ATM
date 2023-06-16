namespace TransactionService.Startup;

internal static class RegisterMiddlewaresExtension
{
    internal static WebApplication RegisterMiddlewares(this WebApplication webApp)
    {
        if (webApp.Environment.IsDevelopment())
        {
            webApp.UseSwagger();
            webApp.UseSwaggerUI();
            webApp.UseDeveloperExceptionPage();
        }
        webApp.UseHttpsRedirection();
        webApp.UseRouting();
        webApp.MapControllers();

        return webApp;
    }
}

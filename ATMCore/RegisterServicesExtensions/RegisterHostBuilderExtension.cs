using ATMCore.Extensions;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ATMCore.RegisterServicesExtensions;

/// <summary>
/// Class for Register Autofac services.
/// </summary>
public static class RegisterHostBuilderExtension
{
    public static void RegisterHostBuilder(this IHostBuilder hostBuilder, string assemblyName, Func<Type, bool> filter)
    {
        hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
        {
            builder.RegisterAssemblyTypesWhere(assemblyName, filter);
        }));
    }
}

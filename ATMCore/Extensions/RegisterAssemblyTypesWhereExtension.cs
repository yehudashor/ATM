using Autofac;
using System.Reflection;

namespace ATMCore.Extensions;

/// <summary>
/// Provides an extension method for containerBuilder (autofac)
/// to register assembly types based on a filter.
/// </summary>
internal static class RegisterAssemblyTypesWhereExtension
{
    public static void RegisterAssemblyTypesWhere(this ContainerBuilder builder,
        string assemblyName, Func<Type, bool> filter)
    {
        var assembly = Assembly.Load(assemblyName);

        builder.RegisterAssemblyTypes(assembly)
            .Where(filter)
            .AsImplementedInterfaces();
    }
}

using System.Reflection;

namespace TransactionService.Exceptions;

public class ExceptionsTypes
{
    public static HashSet<Type> ExceptionsTypesSet { get; } = new HashSet<Type>();

    static ExceptionsTypes()
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        ExceptionsTypesSet = currentAssembly.GetTypes().Where(t => t.BaseType == typeof(BadRequestException)).ToHashSet();
    }
}

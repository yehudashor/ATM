using ATMCore.RabbitMQ.Consumer;
using ATMCore.RabbitMQ.Producer;
using Microsoft.Extensions.DependencyInjection;

namespace ATMCore.RegisterServicesExtensions;

/// <summary>
///  Class for Register RabbitMQ (Producer & Consumer).
/// </summary>
public static class RegisterRabbitMQExtension
{
    public static IServiceCollection RegisterRabbitM(this IServiceCollection services)
    {
        services.AddSingleton<IMessageConsumer, RabbitMQConsumer>();
        services.AddSingleton<IMessageProducer, RabbitMQProducer>();
        return services;
    }
}

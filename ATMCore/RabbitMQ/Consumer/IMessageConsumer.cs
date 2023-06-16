namespace ATMCore.RabbitMQ.Consumer;

public interface IMessageConsumer
{
    void Subscribe<Message>(string queueName, Func<Message, Task<object>> messageHandler = null);
}

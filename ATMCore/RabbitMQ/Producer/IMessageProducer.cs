namespace ATMCore.RabbitMQ.Producer;

public interface IMessageProducer
{
    void SendMessage<Message>(Message message, string queueName, string responseQueueName = "");
}

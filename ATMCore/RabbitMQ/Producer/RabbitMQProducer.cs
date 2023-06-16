using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ATMCore.RabbitMQ.Producer;

public class RabbitMQProducer : IMessageProducer
{
    public void SendMessage<Message>(Message message, string queueName, string responseQueueName = null)
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri(Constants.Constants.RabbitMQConnectionString),
            DispatchConsumersAsync = true
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        IBasicProperties properties = null;
        if (responseQueueName != string.Empty)
        {
            properties = channel.CreateBasicProperties();
            properties.ReplyTo = responseQueueName;
        }

        var serializedMessage = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(serializedMessage);

        channel.QueueDeclare(queue: queueName, durable: false,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null);

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: queueName,
                             basicProperties: properties, body: body);
        // stop();
    }

    //private void stop()
    //{
    //    channel.Close();
    //    connection.Close();
    //}
}

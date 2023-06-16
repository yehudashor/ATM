using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ATMCore.RabbitMQ.Consumer;

public class RabbitMQConsumer : IMessageConsumer
{
    private IConnection _connection;
    private IModel _channel;
    public void Subscribe<Message>(string queueName, Func<Message, Task<object>> messageHandler)
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri(Constants.Constants.RabbitMQConnectionString),
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.BasicQos(0, 1, false);

        _channel.QueueDeclare(queue: queueName,
                    durable: false,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        var response = default(Message);

        consumer.Received += async (sender, args) =>
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            response = JsonConvert.DeserializeObject<Message>(message);

            if (messageHandler is not null && response is not null)
            {
                try
                {
                    var replyData = await messageHandler(response);
                    if (replyData is not null and not string)
                    {
                        replyTo(replyData, args, _channel);
                    }
                    await Console.Out.WriteLineAsync(replyData!.ToString());
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

        //StopAsync();
    }


    private void replyTo(object replyData, BasicDeliverEventArgs deliverEventArgs, IModel channel)
    {

        var replyProperties = channel.CreateBasicProperties();
        replyProperties.CorrelationId = deliverEventArgs.BasicProperties.CorrelationId;

        var replyBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(replyData));

        channel.BasicPublish(exchange: string.Empty, routingKey: deliverEventArgs.BasicProperties.ReplyTo,
            basicProperties: replyProperties, body: replyBody);

    }
}

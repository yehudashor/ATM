using ATMCore.RabbitMQ.Consumer;

namespace ATMCore.RabbitMQ.MessageConsumerExtensions;

public static class MessageConsumerExtensions
{
    public static Response ReceiveWithTimeout<Response>(this IMessageConsumer messageConsumer,
        string responseQueueName, TimeSpan timeout) where Response : class
    {
        var startTime = DateTime.Now;
        Response response = null;

        messageConsumer.Subscribe<Response>(responseQueueName, async (message) =>
        {
            response = message;
            await Task.Delay(0);
            return "";
        });

        while (DateTime.Now - startTime < timeout)
        {
            if (response != null)
            {
                return response;
            }

            Thread.Sleep(100);
        }

        return response;
    }
}

//public interface IMessageReceiveResponse
//{
//    Response ReceiveResponse<Response>(string responseQueueName, TimeSpan timeout);
//}

//public class RabbitMQReceiveResponse : IMessageReceiveResponse
//{
//    private readonly IMessageConsumer _messageConsumer;

//    public RabbitMQReceiveResponse(IMessageConsumer messageConsumer)
//    {
//        _messageConsumer = messageConsumer;
//    }

//    public Response ReceiveResponse<Response>(string responseQueueName, TimeSpan timeout)
//    {
//        return _messageConsumer.ReceiveWithTimeout<Response>(responseQueueName, timeout);
//    }
//}
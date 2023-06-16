namespace ATMCore.Constants;

public class Constants
{
    public const string UpdateAccountQueueName = "AccountUpdate";
    public const string GetAccountQueueNameResponse = "GetAccountResponse";
    public const string GetAccountQueueNameRequest = "GetAccountRequest";
    public const string RabbitMQConnectionString = "amqp://guest:guest@localhost:5672";

    public const int TimeOut = 50;
}

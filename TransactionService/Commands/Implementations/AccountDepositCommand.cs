using ATMCore.RabbitMQ.Producer;
using MediatR;
using TransactionService.AccountServiceClient;
using TransactionService.Commands.Interfaces;
using static TransactionService.Handlers.DepositAccountHandler;

namespace TransactionService.Commands.Implementations;

public class DepositAccountCommand : IDepositAccountCommand
{
    private readonly IMessageProducer _messageProducer;

    private readonly IApiClient _apiClient;

    public DepositAccountCommand(IMessageProducer messageProducer, IApiClient apiClient)
    {
        _messageProducer = messageProducer;
        _apiClient = apiClient;
    }

    public async Task AccountDeposit(DepositCommand depositCommand)
    {
        #region RabbitMQ
        //var updateQueueName = ATMCore.Constants.Constants.UpdateAccountQueueName;
        //var getQueueName = ATMCore.Constants.Constants.GetAccountQueueNameResponse;
        //_messageProducer.SendMessage(depositCommand, updateQueueName, getQueueName);
        // Console.WriteLine(depositCommand.AccountId + " " + depositCommand.Amount.ToString());
        #endregion

        await _apiClient.Put(Constants.Constants.updateAccountUrl, depositCommand);
    }

    public class DepositCommand : IRequest<DepositAccountResult>
    {
        public int AccountId { get; set; }
        public long Amount { get; set; }
    }
}

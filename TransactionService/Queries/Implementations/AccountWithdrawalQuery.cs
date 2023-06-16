using ATMCore.RabbitMQ.Producer;
using MediatR;
using TransactionService.AccountServiceClient;
using TransactionService.Queries.Interfaces;
using static TransactionService.Handlers.AccountWithdrawalHandler;

namespace TransactionService.Queries.Implementations;

public class AccountWithdrawalQuery : IAccountWithdrawalQuery
{
    private readonly IMessageProducer _messageProducer;
    private readonly IApiClient _apiClient;

    public AccountWithdrawalQuery(IMessageProducer messageProducer, IApiClient apiClient)
    {
        _messageProducer = messageProducer;
        _apiClient = apiClient;
    }

    public async Task AccountWithdrawal(WithdrawalQuery withdrawalQuery)
    {
        withdrawalQuery.Amount = -withdrawalQuery.Amount;
        await _apiClient.Put(Constants.Constants.updateAccountUrl, withdrawalQuery);

        #region RabbitMq
        //_messageProducer.SendMessage(withdrawalQuery,
        //    ATMCore.Constants.Constants.UpdateAccountQueueName);
        //Console.WriteLine(withdrawalQuery.AccountId + " " + withdrawalQuery.Amount.ToString());
        #endregion
    }

    public class WithdrawalQuery : IRequest<WithdrawalAccountResult>
    {
        public int AccountId { get; set; }
        public long Amount { get; set; }
    }
}

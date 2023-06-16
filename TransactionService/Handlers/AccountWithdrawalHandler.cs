using ATMCore.RabbitMQ.Consumer;
using ATMCore.RabbitMQ.Producer;
using MediatR;
using TransactionDataModel.Models.Entities.Enums;
using TransactionService.AccountServiceClient;
using TransactionService.ATMOperations.Withdrawal;
using TransactionService.Exceptions;
using TransactionService.Queries.Interfaces;
using static TransactionService.Handlers.AccountWithdrawalHandler;
using static TransactionService.Queries.Implementations.AccountWithdrawalQuery;

namespace TransactionService.Handlers;

public class AccountWithdrawalHandler : IRequestHandler<WithdrawalQuery, WithdrawalAccountResult>
{
    private readonly IAccountWithdrawalQuery _accountWithdrawalQuery;
    private readonly IATMWithdrawal _aTMWithdrawal;
    private readonly IApiClient _apiClient;

    private readonly IMessageProducer _messageProducer;
    private readonly IMessageConsumer _messageConsumer;

    public AccountWithdrawalHandler(IAccountWithdrawalQuery accountWithdrawalCommand, IMessageConsumer messageConsumer,
        IATMWithdrawal aTMWithdrawal, IMessageProducer messageProducer, IApiClient apiClient)
    {
        _accountWithdrawalQuery = accountWithdrawalCommand;
        _messageConsumer = messageConsumer;
        _aTMWithdrawal = aTMWithdrawal;
        _messageProducer = messageProducer;
        _apiClient = apiClient;
    }

    /// <summary>
    /// Handles the withdrawal request.
    /// </summary>
    /// <param name="withdrawalQuery">The withdrawal query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The withdrawal account result.</returns>
    public async Task<WithdrawalAccountResult> Handle(WithdrawalQuery withdrawalQuery, CancellationToken cancellationToken)
    {
        var requestUrl = $"{Constants.Constants.getAccountUrl}{withdrawalQuery.AccountId}";

        var currentAccount = await processWithdrawal(withdrawalQuery, requestUrl);

        var newAccount = await processWithdrawalResult(withdrawalQuery, currentAccount, requestUrl);

        return newAccount;

        #region rabbitMQ
        //var queueNameRequest = ATMCore.Constants.Constants.GetAccountQueueNameRequest;
        //var queueNameResponse = ATMCore.Constants.Constants.GetAccountQueueNameResponse;
        //var timeOut = TimeSpan.FromSeconds(ATMCore.Constants.Constants.timeOut);

        //_messageProducer.SendMessage(withdrawalCommand, queueNameRequest, queueNameResponse);

        //WithdrawalAccountResult oldAccount = await Task.Run(() => _messageConsumer.ReceiveWithTimeout<WithdrawalAccountResult>(queueNameResponse, timeOut));

        //WithdrawalAccountResult newAccount = null;

        //if (oldAccount is not null)
        //{
        //    await processWithdrawal(withdrawalCommand, oldAccount);

        //    newAccount = await Task.Run(() => _messageConsumer.ReceiveWithTimeout<WithdrawalAccountResult>(queueNameResponse, timeOut));
        //    if (newAccount is not null)
        //    {
        //        await processWithdrawalResult(withdrawalCommand, oldAccount, newAccount);
        //    }
        //}

        //return newAccount!;
        #endregion
    }

    /// <summary>
    /// Processes the withdrawal of funds from the account.
    /// </summary>
    /// <param name="withdrawalQuery">The withdrawal query containing account and amount information.</param>
    /// <param name="requestUrl">The url for retrieving the current account details.</param>
    /// <returns>The current account details before the withdrawal.</returns>
    private async Task<WithdrawalAccountResult> processWithdrawal(WithdrawalQuery withdrawalQuery, string requestUrl)
    {
        await _aTMWithdrawal.InitBillsForWithdrawal(withdrawalQuery);

        // Retrieve the current account details
        var currentAccount = await _apiClient.Get<WithdrawalAccountResult>(requestUrl);

        // Check if the account has sufficient balance for withdrawal
        if (currentAccount.Balance - withdrawalQuery.Amount > 0)
        {
            await _accountWithdrawalQuery.AccountWithdrawal(withdrawalQuery);

            return currentAccount;
        }

        // Throw an exception indicating insufficient funds
        throw new InsufficientFundsException();
    }

    /// <summary>
    /// Processes the withdrawal result after the withdrawal transaction is completed.
    /// </summary>
    /// <param name="withdrawalQuery">The withdrawal query containing account and amount information.</param>
    /// <param name="currentAccount">The current account details before the withdrawal.</param>
    /// <param name="requestUrl">The URL for retrieving the updated account details.</param>
    /// <returns>The updated account details after the withdrawal.</returns>
    private async Task<WithdrawalAccountResult> processWithdrawalResult(WithdrawalQuery withdrawalQuery,
        WithdrawalAccountResult currentAccount, string requestUrl)
    {
        // Retrieve the updated account details
        var newAccount = await _apiClient.Get<WithdrawalAccountResult>(requestUrl);

        // Check if the withdrawal transaction was successful and update the account details
        if (newAccount.Balance == currentAccount!.Balance + withdrawalQuery.Amount)
        {
            newAccount.Bills = await _aTMWithdrawal.Withdrawal();
            return newAccount;
        }
        throw new WithdrawalProcessingException();
    }

    #region RabbitMQ

    //private async Task processWithdrawalResult(WithdrawalQuery withdrawalCommand, WithdrawalAccountResult? oldAccount, WithdrawalAccountResult newAccount)
    //{
    //    if (newAccount.Balance == oldAccount!.Balance + withdrawalCommand.Amount)
    //    {
    //        newAccount.Bills = await _aTMWithdrawal.Withdrawal();
    //        Console.WriteLine(newAccount.Balance + " ");
    //    }
    //}

    //private async Task processWithdrawal(WithdrawalQuery withdrawalCommand, WithdrawalAccountResult oldAccount)
    //{
    //    if (oldAccount.Balance - withdrawalCommand.Amount > 0)
    //    {
    //        await _aTMWithdrawal.InitBillsForWithdrawal(withdrawalCommand);

    //        withdrawalCommand.Amount *= -1;

    //        _accountWithdrawalCommand.AccountWithdrawal(withdrawalCommand);
    //    }
    //}
    #endregion

    /// <summary>
    /// Represents the result of a withdrawal operation.
    /// </summary>
    public class WithdrawalAccountResult
    {
        public long Balance { init; get; }
        public Dictionary<BillType, long> Bills { set; get; }
    }
}
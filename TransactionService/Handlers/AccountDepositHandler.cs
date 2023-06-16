using ATMCore.RabbitMQ.Consumer;
using MediatR;
using TransactionService.AccountServiceClient;
using TransactionService.ATMOperations.Deposit;
using TransactionService.Commands.Interfaces;
using TransactionService.Exceptions;
using static TransactionService.Commands.Implementations.DepositAccountCommand;

namespace TransactionService.Handlers;

public class DepositAccountHandler : IRequestHandler<DepositCommand, DepositAccountHandler.DepositAccountResult>
{
    private readonly IDepositAccountCommand _depositAccountCommand;
    private readonly IMessageConsumer _messageConsumer;
    private readonly IATMDeposit _aTMDeposit;
    private readonly IApiClient _apiClient;

    public DepositAccountHandler(IDepositAccountCommand depositAccountCommand, IMessageConsumer messageConsumer, IATMDeposit aTMDeposit, IApiClient apiClient)
    {
        _depositAccountCommand = depositAccountCommand;
        _messageConsumer = messageConsumer;
        _aTMDeposit = aTMDeposit;
        _apiClient = apiClient;
    }

    /// <summary>
    /// Processes the deposit of funds from the account.
    /// </summary>
    /// <param name="withdrawalQuery">The deposit query containing account id.</param>
    /// <param name="requestUrl">The url for retrieving the current account details.</param>
    /// <returns>The current account details before the withdrawal.</returns>
    public async Task<DepositAccountResult> Handle(DepositCommand depositCommand, CancellationToken cancellationToken)
    {
        #region RabbitMQ
        //_depositAccountCommand.AccountDeposit(depositCommand);

        //var timeOut = TimeSpan.FromSeconds(ATMCore.Constants.Constants.timeOut);
        //var queueNameResponse = ATMCore.Constants.Constants.GetAccountQueueNameResponse;

        //DepositAccountResult newAccount = await Task.Run(() => _messageConsumer.ReceiveWithTimeout
        //    <DepositAccountResult>(queueNameResponse, timeOut));

        //if (newAccount is not null)
        //{
        //    Console.WriteLine(newAccount.Balance + " ");
        //    await _aTMDeposit.Deposit(depositCommand);
        //}
        //return newAccount!;
        #endregion

        // Deposit account command
        await _depositAccountCommand.AccountDeposit(depositCommand);

        // Get account url.
        var url = $"{Constants.Constants.getAccountUrl}{depositCommand.AccountId}";

        // Get account new balance.
        DepositAccountResult newAccount = await _apiClient.Get<DepositAccountResult>(url);

        if (newAccount is null)
        {
            throw new TransactionProcessingException();
        }

        // Perform the deposit operation
        await _aTMDeposit.Deposit(depositCommand);

        // Return the new account information
        return newAccount;
    }

    public class DepositAccountResult
    {
        public long Balance { init; get; }
    }
}

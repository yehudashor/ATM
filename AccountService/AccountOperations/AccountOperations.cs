using ATMCore.RabbitMQ.Consumer;
using ATMCore.RabbitMQ.Producer;
using MediatR;
using static AccountService.Commands.Implementations.UpdateAccountCommand;
using static AccountService.Handlers.GetAccountBalanceHandler;
using static AccountService.Queries.Implementations.GetAccountQuery;

namespace AccountService.AccountOperations;

public class AccountOperations
{
    private readonly IMessageConsumer _messageConsumer;
    private readonly IMessageProducer _messageProducer;
    private readonly IMediator _mediator;

    public AccountOperations(IMessageConsumer messageConsumer, IMessageProducer messageProducer, IMediator mediator)
    {
        _messageConsumer = messageConsumer;
        _messageProducer = messageProducer;
        _mediator = mediator;
        updateAccount();
        getAccountSubscriber();
    }

    private void getAccountSubscriber()
    {
        _messageConsumer.Subscribe<AccountQuery>
            (ATMCore.Constants.Constants.GetAccountQueueNameRequest,
            async (accountCommand) =>
            {
                var result = await _mediator.Send(accountCommand);
                return result;
            });
    }

    private async Task<AccountQueryResult> getAccountResult(int accountId)
    {
        var accountQuery = new AccountQuery { AccountId = accountId };
        var accountQueryResult = await _mediator.Send(accountQuery);
        Console.WriteLine(accountQueryResult.Balance + " ");

        return accountQueryResult;
    }

    private void updateAccount()
    {
        _messageConsumer.Subscribe<AccountCommand>
            (ATMCore.Constants.Constants.UpdateAccountQueueName,
            async (accountCommand) =>
            {
                await _mediator.Send(accountCommand);
                return await getAccountResult(accountCommand.AccountId);
            });
    }
}

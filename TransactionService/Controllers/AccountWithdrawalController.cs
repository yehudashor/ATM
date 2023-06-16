using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionService.Exceptions;
using static TransactionService.Queries.Implementations.AccountWithdrawalQuery;

namespace TransactionService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountWithdrawalController : ControllerBase
{
    private readonly IMediator _mediator;
    public AccountWithdrawalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet, Route("accountId{accountId:int}/amount{amount:long}")]
    public async Task<IActionResult> WithdrawalAccount([FromHeader] WithdrawalQuery withdrawalQuery)
    {
        try
        {
            var result = await _mediator.Send(withdrawalQuery);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex) when (ex is not null && ExceptionsTypes.ExceptionsTypesSet.Contains(ex.GetType()))
        {
            return BadRequest(ex.Message);
        }
    }
}

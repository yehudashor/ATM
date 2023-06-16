using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionService.Exceptions;
using static TransactionService.Commands.Implementations.DepositAccountCommand;

namespace TransactionService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountDepositController : ControllerBase
{
    private readonly IMediator _mediator;
    public AccountDepositController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch]
    public async Task<IActionResult> DepositAccount([FromBody] DepositCommand deposit)
    {
        try
        {
            var result = await _mediator.Send(deposit);
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

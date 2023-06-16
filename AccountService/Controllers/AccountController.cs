using AccountService.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static AccountService.Commands.Implementations.UpdateAccountCommand;
using static AccountService.Queries.Implementations.GetAccountQuery;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the account information for the specified account ID.
        /// </summary>
        /// <param name="accountId">The account ID to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>The account information if found, or an appropriate error response.</returns>
        [HttpGet("accountId/{accountId:int}")]
        public async Task<IActionResult> GetAccount([FromRoute] int accountId)
        {
            var accountQuery = new AccountQuery { AccountId = accountId };

            try
            {
                var account = await _mediator.Send(accountQuery);
                return Ok(account);
            }
            catch (AccountNotExistException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the account information based on the specified account command.
        /// </summary>
        /// <param name="accountCommand">The account command to execute.</param>
        /// <returns>An appropriate response indicating the success or failure of the update.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountCommand accountCommand)
        {
            try
            {
                if (accountCommand is null)
                {
                    return BadRequest();
                }

                await _mediator.Send(accountCommand);
                return Ok(accountCommand);
            }
            catch (AccountNotExistException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

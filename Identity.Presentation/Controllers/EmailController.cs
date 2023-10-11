using Identity.Application.Features.Emails.Commands.ConfirmEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultLibrary.Results.Error;

namespace Identity.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly ISender _sender;

    public EmailController(ISender sender)
    {
        _sender = sender;
    }
    
    /// <summary>
    /// Confirms an email address with the provided confirmation code.
    /// </summary>
    /// <param name="command">The confirmation command with email and code.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>No content if email confirmation is successful.</returns>
    /// <response code="204">Email address confirmed successfully.</response>
    /// <response code="400">Bad request due to incorrect email confirmation code.</response>
    /// <response code="404">Email or confirmation code not found.</response>
    [AllowAnonymous]
    [HttpGet("confirm")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IncorrectErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Confirm(
        [FromQuery] ConfirmEmailCommand command,
        CancellationToken cancellationToken)
    {
        await _sender.Send(command, cancellationToken);

        return NoContent();
    }
}
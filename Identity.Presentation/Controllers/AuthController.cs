using Identity.Application.Features.Users.Commands.RegisterUser;
using Identity.Application.Features.Users.Queries.LoginUser;
using Identity.Application.Models;
using Identity.Presentation.ErrorResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultLibrary.Results.Error;

namespace Identity.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="command">Data for user registration.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>Registration operation result.</returns>
    /// <response code="201">User registered successfully.</response>
    /// <response code="409">User with the provided data already exists.</response>
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ModelExistErrorResult), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> Register(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        await _sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Login), null);
    }

    /// <summary>
    /// Logs a user into the system.
    /// </summary>
    /// <param name="model">User login data.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>User authentication token.</returns>
    /// <response code="200">User successfully logged into the system.</response>
    /// <response code="400">Incorrect user credentials.</response>
    /// <response code="404">User not found.</response>
    /// <response code="403">User account not confirmed.</response>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PasswordNotEqualErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(EmailNotConfirmedErrorResult), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<TokenViewModel>> Login(
        [FromBody] LoginUserQuery model,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(model, cancellationToken);

        return Ok(result);
    }
}
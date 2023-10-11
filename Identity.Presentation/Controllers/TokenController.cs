using AutoMapper;
using Identity.Application.Features.Tokens.Commands.RefreshUserToken;
using Identity.Application.Features.Tokens.Commands.RevokeAllUserToken;
using Identity.Application.Features.Tokens.Commands.RevokeUserToken;
using Identity.Application.Models;
using Identity.Application.Services.Interfaces;
using Identity.Presentation.ErrorResult;
using Identity.Presentation.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultLibrary.Results.Error;

namespace Identity.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IClaimManager<UserClaimModel> _userClaimManager;
    private readonly IMapper _mapper;

    public TokenController(
        ISender sender,
        IClaimManager<UserClaimModel> userClaimManager,
        IMapper mapper)
    {
        _userClaimManager = userClaimManager;
        _mapper = mapper;
        _sender = sender;
    }

    /// <summary>
    /// Retrieves the user's claim information.
    /// </summary>
    /// <returns>The user's claim information.</returns>
    /// <response code="200">User claim information retrieved successfully.</response>
    /// <response code="401">Unauthorized request due to token validation failure.</response>
    [Authorize]
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(UserClaimModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(TokenValidationErrorResult), StatusCodes.Status401Unauthorized)]
    public ActionResult<UserClaimModel> Get()
    {
        var userClaimModel = _userClaimManager.ToClaimModel(User.Claims);
        return Ok(userClaimModel);
    }
    
    /// <summary>
    /// Refreshes the user's token with a new one.
    /// </summary>
    /// <param name="command">Data for refreshing the user's token.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The refreshed token.</returns>
    /// <response code="200">Token refreshed successfully.</response>
    /// <response code="401">Unauthorized request due to token validation failure.</response>
    [AllowAnonymous]
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokenViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(TokenValidationErrorResult), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenViewModel>> Refresh(
        [FromBody] UserRefreshTokenCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Revokes a user's specific token.
    /// </summary>
    /// <param name="model">Data for revoking the user's token.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>No content if the token is revoked successfully.</returns>
    /// <response code="204">Token revoked successfully.</response>
    /// <response code="401">Unauthorized request due to token validation failure.</response>
    /// <response code="404">User or token not found.</response>
    [Authorize]
    [HttpDelete("revoke")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(TokenValidationErrorResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Revoke(
        [FromQuery] RevokeUserTokenDataModel model,
        CancellationToken cancellationToken)
    {
        var userClaimModel = _userClaimManager.ToClaimModel(User.Claims);
        
        var command = _mapper.Map<RevokeUserTokenCommand>(model);
        command.UserId = userClaimModel.Id;

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Revokes all tokens for the user.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>No content if all tokens are revoked successfully.</returns>
    /// <response code="204">All user tokens revoked successfully.</response>
    /// <response code="401">Unauthorized request due to token validation failure.</response>
    /// <response code="404">User not found.</response>
    [Authorize]
    [HttpDelete("revoke/all")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(TokenValidationErrorResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    // UserNotFoundByIdException
    public async Task<ActionResult> RevokeAll(CancellationToken cancellationToken)
    {
        var userClaimModel = _userClaimManager.ToClaimModel(User.Claims);
        var command = new RevokeAllUserTokenCommand { UserId = userClaimModel.Id };

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }
}
using AutoMapper;
using Identity.Application.Features.Passwords.Commands.ChangeUserPassword;
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
public class PasswordController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IClaimManager<UserClaimModel> _claimManager;
    private readonly IMapper _mapper;

    public PasswordController(
        ISender sender,
        IClaimManager<UserClaimModel> claimManager,
        IMapper mapper)
    {
        _claimManager = claimManager;
        _mapper = mapper;
        _sender = sender;
    }

    /// <summary>
    /// Changes the password for the current user.
    /// </summary>
    /// <param name="model">Data for changing the user's password.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>No content if the password is changed successfully.</returns>
    /// <response code="204">Password changed successfully.</response>
    /// <response code="400">Bad request due to an incorrect password or invalid data.</response>
    /// <response code="401">Unauthorized request due to token validation failure.</response>
    /// <response code="404">User not found.</response>
    [Authorize]
    [HttpPost("change")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(PasswordNotEqualErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(TokenValidationErrorResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangePassword(
        [FromBody] ChangeUserPasswordDataModel model,
        CancellationToken cancellationToken)
    {
        var userClaimModel = _claimManager.ToClaimModel(User.Claims);

        var command = _mapper.Map<ChangeUserPasswordCommand>(model);
        command.UserId = userClaimModel.Id;

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }
}
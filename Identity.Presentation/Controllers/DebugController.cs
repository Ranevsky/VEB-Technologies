using Identity.Application.Models;
using Identity.Application.Repositories.Interfaces;
using Identity.Application.Services.Interfaces;
using Identity.Presentation.ErrorResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultLibrary.Results.Error;

namespace Identity.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class DebugController : ControllerBase
{
    private readonly IClaimManager<UserClaimModel> _userClaimManager;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public DebugController(
        IClaimManager<UserClaimModel> userClaimManager,
        IRoleRepository roleRepository,
        IUserRepository userRepository)
    {
        _userClaimManager = userClaimManager;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }
    
    /// <summary>
    /// Sets a user's role.
    /// </summary>
    /// <param name="role">The role to assign to the user.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>No content if the role is set successfully.</returns>
    /// <response code="204">Role set successfully.</response>
    /// <response code="401">Unauthorized request due to token validation failure.</response>
    /// <response code="404">Role or user not found.</response>
    [Authorize]
    [HttpPut("role")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(TokenValidationErrorResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> SetRole(
        string role,
        CancellationToken cancellationToken)
    {
        var claimModel = _userClaimManager.ToClaimModel(User.Claims);

        if (claimModel.Roles.Any(roleClaim =>
            string.Equals(roleClaim, role, StringComparison.InvariantCultureIgnoreCase)))
        {
            return Ok();
        }

        var roleDb = await _roleRepository.GetByNameAsync(role, cancellationToken);
        await _userRepository.AddRoleAsync(claimModel.Id, roleDb.Id, cancellationToken);

        return NoContent();
    }
}
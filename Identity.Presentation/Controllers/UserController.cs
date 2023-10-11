using AutoMapper;
using Identity.Application.Features.Users.Commands.AddRoleToUser;
using Identity.Application.Features.Users.Commands.RemoveUser;
using Identity.Application.Features.Users.Commands.UpdateUser;
using Identity.Application.Features.Users.Queries.GetUser;
using Identity.Application.Models;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using Identity.Presentation.ErrorResult;
using Identity.Presentation.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultLibrary.Results.Error;

namespace Identity.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IClaimManager<UserClaimModel> _userClaimManage;
    private readonly IMapper _mapper;

    public UserController(
        ISender sender,
        IClaimManager<UserClaimModel> userClaimManage,
        IMapper mapper)
    {
        _sender = sender;
        _userClaimManage = userClaimManage;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves user information by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve information for.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>User information if found.</returns>
    /// <response code="200">User information retrieved successfully.</response>
    /// <response code="404">User not found by ID.</response>
    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserInfoViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserInfoViewModel>> Get(
        [FromRoute] string id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserQuery { Id = id };
        var userInfo = await _sender.Send(query, cancellationToken);

        return Ok(userInfo);
    }
    
    /// <summary>
    /// Updates user information.
    /// </summary>
    /// <param name="model">Data for updating user information.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>No content if user information is updated successfully.</returns>
    /// <response code="204">User information updated successfully.</response>
    /// <response code="401">Unauthorized request due to token validation failure.</response>
    /// <response code="404">User not found by ID.</response>
    [Authorize]
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(TokenValidationErrorResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        UpdateUserDataModel model,
        CancellationToken cancellationToken)
    {
        var claimModel = _userClaimManage.ToClaimModel(User.Claims);
        
        var command = _mapper.Map<UpdateUserCommand>(model);
        command.Id = claimModel.Id;
        
        await _sender.Send(command, cancellationToken);

        return NoContent();
    }
    
    /// <summary>
    /// Removes a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to remove.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>No content if the user is removed successfully.</returns>
    /// <response code="204">User removed successfully.</response>
    /// <response code="401">Unauthorized request due to token validation failure.</response>
    /// <response code="403">Forbidden request, user lacks necessary permissions.</response>
    /// <response code="404">User not found by ID.</response>
    [Authorize(Roles = RoleEntity.SuperAdmin)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(TokenValidationErrorResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(TokenValidationErrorResult), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Remove(
        [FromRoute] string id,
        CancellationToken cancellationToken)
    {
        var command = new RemoveUserCommand { Id = id };
        await _sender.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Adds a role to a user.
    /// </summary>
    /// <param name="command">Data for adding a role to a user.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>No content if the role is added successfully.</returns>
    /// <response code="204">Role added to the user successfully.</response>
    /// <response code="404">User or role not found by ID.</response>
    [HttpPost("role")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddRole(
        [FromBody] AddRoleToUserCommand command,
        CancellationToken cancellationToken)
    {
        await _sender.Send(command, cancellationToken);

        return NoContent();
    }
}
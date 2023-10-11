using Identity.Application.Features.Role.Commands.CreateRole;
using Identity.Application.Features.Role.Queries.GetAllRoles;
using Identity.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResultLibrary.AspNetCore.Results.Action;
using ResultLibrary.Models;
using ResultLibrary.Results.Error;

namespace Identity.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly ISender _sender;

    public RoleController(ISender sender)
    {
        _sender = sender;
    }
    
    /// <summary>
    /// Retrieves a list of all user roles.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A list of user roles.</returns>
    /// <response code="200">List of user roles retrieved successfully.</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<RoleInfoView>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RoleInfoView>>> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllRolesQuery();
        var roles = await _sender.Send(query, cancellationToken);

        return Ok(roles);
    }
    
    /// <summary>
    /// Creates a new user role.
    /// </summary>
    /// <param name="command">Data for creating a new role.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The ID of the newly created role.</returns>
    /// <response code="201">Role created successfully.</response>
    /// <response code="409">Conflict, a role with the same name already exists.</response>
    [HttpPost]
    [ProducesResponseType(typeof(IdViewModel<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ModelExistErrorResult), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<IdViewModel<string>>> Create(
        [FromBody] CreateRoleCommand command,
        CancellationToken cancellationToken)
    {
        var roleId = await _sender.Send(command, cancellationToken);

        return new CreatedIdResult<string>(roleId);
    }
}
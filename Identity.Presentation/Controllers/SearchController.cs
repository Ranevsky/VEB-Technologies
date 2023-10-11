using Identity.Application.Features.Users.Queries.GetUserCatalog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISender _sender;

    public SearchController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Searches for users based on the provided query.
    /// </summary>
    /// <param name="query">The search query to find users.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A list of users matching the search query.</returns>
    /// <response code="200">List of users retrieved successfully.</response>
    [HttpGet("user")]
    [ProducesResponseType(typeof(UserCatalogViewModel), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserCatalogViewModel>> Search(
        [FromQuery] GetUserCatalogQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }
}
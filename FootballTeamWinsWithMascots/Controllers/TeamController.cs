using FootballTeamWinsWithMascots.Application.Commond.Models;
using FootballTeamWinsWithMascots.Application.Dtos;
using FootballTeamWinsWithMascots.Application.Request.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballTeamWinsWithMascots.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Searches teams by query text and optional columns list.
        /// </summary>
        /// <remarks>
        /// POST /api/teams/search
        /// Body example:
        /// {
        ///   "query": "hawk",
        ///   "columns": ["Name","Mascot"],   // omit or empty => all allowed
        ///   "page": 1,
        ///   "pageSize": 25
        /// }
        /// </remarks>
        [HttpPost("search")]
        [ProducesResponseType(typeof(PagedResult<TeamDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PagedResult<TeamDto>>> Search(
            [FromBody] SearchTeamsQuery query,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(query, cancellationToken);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return Problem(
                    title: "Invalid request",
                    detail: ex.Message,
                    statusCode: 400);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Unexpected error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }
    }
}

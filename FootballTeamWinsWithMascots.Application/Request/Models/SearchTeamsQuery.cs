using FootballTeamWinsWithMascots.Application.Commond.Models;
using FootballTeamWinsWithMascots.Application.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FootballTeamWinsWithMascots.Application.Request.Models
{
    public class SearchTeamsQuery : IRequest<PagedResult<TeamDto>>
    {
        public string Query { get; set; } = "";
        public IReadOnlyList<string>? Columns  { get; set; }
        public int Page { get; set; } = 1;
        [Range(1, 100)]
        public int PageSize { get; set; } = 20;
    }
}
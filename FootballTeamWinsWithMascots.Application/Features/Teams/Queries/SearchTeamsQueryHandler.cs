using FootballTeamWinsWithMascots.Application.Commond.Models;
using FootballTeamWinsWithMascots.Application.Dtos;
using FootballTeamWinsWithMascots.Application.Request.Models;
using FootballTeamWinsWithMascots.Domain.Interfaces.ReadRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballTeamWinsWithMascots.Application.Features.Teams.Queries
{
    public class SearchTeamsQueryHandler : IRequestHandler<SearchTeamsQuery, PagedResult<TeamDto>>
    {
        // Define allowed columns for searching
        // We only allow "Name" and "Mascot" to be searched
        // If we need to add more searchable columns in the future, we can update this list
        private static readonly string[] AllowedColumns = { "Name", "Mascot"};
        private readonly ITeamsReadRepository _teamsReadRepository;

        public SearchTeamsQueryHandler(ITeamsReadRepository teamsReadRepository)
        {
            _teamsReadRepository = teamsReadRepository;
        }

        public async Task<PagedResult<TeamDto>> Handle(SearchTeamsQuery request, CancellationToken cancellationToken)
        {
            var query = (request.Query ?? "").Trim();

            //Aply allowed columns filter
            var columns = request.Columns?
                .Where(c => AllowedColumns.Contains(c, StringComparer.OrdinalIgnoreCase))
                .ToList() 
                ?? AllowedColumns.ToList();

            //Get Iqueryable from repository
            var teamsQuery = _teamsReadRepository.SearchTeam();

            //Apply search filter
            var like = $"%{query.ToLower()}%";
            // Build dynamic where clause based on requested columns
            // Note: EF.Functions.Like is used for case-insensitive search in SQLite
            // This search only works on the columns specified in the request
            teamsQuery = teamsQuery.Where(team =>
                (columns.Contains("Name", StringComparer.OrdinalIgnoreCase) &&
                 EF.Functions.Like((team.Name ?? "").ToLower(), like)) ||
                (columns.Contains("Mascot", StringComparer.OrdinalIgnoreCase) &&
                 EF.Functions.Like((team.Mascot ?? "").ToLower(), like))
            );


            //calculate total count
            var total = await teamsQuery.CountAsync(cancellationToken);

            //Execute query with pagination
            var teams = await teamsQuery
                .OrderBy(t => t.Rank)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(team => new TeamDto
                (
                    team.Id,
                    team.Rank,
                    team.Name,
                    team.Mascot,
                    team.DateOfLastWin,
                    team.WinsPercentage,
                    team.Wins,
                    team.Losses,
                    team.Ties,
                    team.Games
                ))
                .ToListAsync(cancellationToken);

            return new PagedResult<TeamDto>(total, request.Page, request.PageSize, teams);
        }
    }
}

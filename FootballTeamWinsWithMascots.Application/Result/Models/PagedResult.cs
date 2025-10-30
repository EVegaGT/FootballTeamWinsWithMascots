namespace FootballTeamWinsWithMascots.Application.Commond.Models
{
    public record PagedResult<T>(
        int Total, int Page, int PageSize, IReadOnlyList<T> Items);
}

using FootballTeamWinsWithMascots.Domain.Entities;

namespace FootballTeamWinsWithMascots.Domain.Interfaces.ReadRepositories
{
    public interface ITeamsReadRepository
    {
        IQueryable<Team> SearchTeam();
    }
}
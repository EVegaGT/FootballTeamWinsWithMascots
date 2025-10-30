using FootballTeamWinsWithMascots.Domain.Entities;
using FootballTeamWinsWithMascots.Domain.Interfaces.ReadRepositories;
using FootballTeamWinsWithMascots.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FootballTeamWinsWithMascots.Infrastructure.Repositories
{
    public class TeamsReadRepository : ITeamsReadRepository
    {
        private readonly FootballTeamDbContext _dbContext;
       
        public TeamsReadRepository(FootballTeamDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Team>  SearchTeam()
        {
            return _dbContext.Teams.OrderBy(t => t.Rank).AsNoTracking();
        }  
    }
}

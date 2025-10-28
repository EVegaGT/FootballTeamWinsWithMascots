using FootballTeamWinsWithMascots.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballTeamWinsWithMascots.Infrastructure.ModelsConfiguations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("teams");
            builder.HasKey(o => o.Id);
        }
    }
}
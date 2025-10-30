using FootballTeamWinsWithMascots.Domain.Entities;
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
            builder.Property(o => o.Name).HasMaxLength(300).IsRequired();
            builder.Property(o => o.Mascot).HasMaxLength(300).IsRequired();
            builder.Property(o => o.Rank).IsRequired();
        }
    }
}
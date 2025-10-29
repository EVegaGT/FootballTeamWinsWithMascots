namespace FootballTeamWinsWithMascots.Infrastructure.Migrations.Seed
{
    public interface ICsvTeamsSeeder
    {
        Task SeedData(CancellationToken ct);
    }
}

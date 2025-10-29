using CsvHelper.Configuration;

namespace FootballTeamWinsWithMascots.Infrastructure.Migrations.Seed
{
    public sealed class TeamCsvRowMap : ClassMap<TeamCsvRows>
    {
        public TeamCsvRowMap()
        {
            Map(m => m.Rank).Name("Rank");
            Map(m => m.Name).Name("Team");
            Map(m => m.Mascot).Name("Mascot");
            Map(m => m.DateOfLastWin).Name("Date of Last Win");
            Map(m => m.WinsPercentage).Name("Winning Percetnage");
            Map(m => m.Wins).Name("Wins");
            Map(m => m.Losses).Name("Losses");
            Map(m => m.Ties).Name("Ties");
            Map(m => m.Games).Name("Games");
        }
    }
}

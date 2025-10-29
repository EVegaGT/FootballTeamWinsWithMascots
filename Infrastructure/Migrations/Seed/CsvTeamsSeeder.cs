using CsvHelper;
using CsvHelper.Configuration;
using FootballTeamWinsWithMascots.Infrastructure.DbContexts;
using FootballTeamWinsWithMascots.Infrastructure.Helper;
using FootballTeamWinsWithMascots.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace FootballTeamWinsWithMascots.Infrastructure.Migrations.Seed
{
    public class CsvTeamsSeeder : ICsvTeamsSeeder
    {
        private readonly FootballTeamDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<CsvTeamsSeeder> _logger;

        public CsvTeamsSeeder(FootballTeamDbContext dbContext,
            IConfiguration config,
            ILogger<CsvTeamsSeeder> logger)
        {
            _dbContext = dbContext;
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Retrieved data from a CSV file and save into the Teams table
        /// </summary>
        /// <param name="ct"></param>
        public async Task SeedData(CancellationToken ct = default)
        {
            var seedConfig = _config.GetSection("Seed");
           
            //Validate if the seed is enabled in the app configuration
            bool.TryParse(seedConfig.GetSection("Enabled").Value, out bool enabled);
            if (!enabled)
            {
                _logger.LogInformation("Database seed is disabled. Skipping.");
                return;
            }

            var csvPath = seedConfig.GetSection("CsvPath").Value ?? "Data\\CollegeFootballTeamWinsWithMascots.csv";
            var delimiter = seedConfig.GetSection("Delimiter").Value ?? ",";

            if (!File.Exists(csvPath))
            {
                _logger.LogWarning("CSV not found at {CsvPath}. Seed skipped.", csvPath);
                return;
            }

            //Validate if we already have data in the Teams table
            if (await _dbContext.Teams.AnyAsync(ct))
            {
                _logger.LogInformation("Teams table already has data. Seed skipped.");
                return;
            }

            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = delimiter,
                BadDataFound = null,
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim,
            };

            var rows = GetRows(csvPath, csvConfiguration);
            
            if (rows == null || !rows.Any())
            {
                _logger.LogWarning("No data found in CSV at {CsvPath}. Seed skipped.", csvPath);
                return;
            }

            await sendToDatabase(rows, ct);
        }

        // Read CSV rows
        private IEnumerable<TeamCsvRows> GetRows(string csvPath, CsvConfiguration csvConfiguration)
        {
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, csvConfiguration);
            csv.Context.RegisterClassMap<TeamCsvRowMap>();
            var rows = csv.GetRecords<TeamCsvRows>();
            foreach (var r in rows)
            {
                yield return r;
            }
        }

        // Send valid rows to database
        private async Task sendToDatabase (IEnumerable<TeamCsvRows> rows, CancellationToken ct)
        {
            int line = 0;
            int validRows = 0;
            int invalidRows = 0;

            foreach (var row in rows) {
                
                line++;
                try
                {
                    if (string.IsNullOrWhiteSpace(row.Name) || string.IsNullOrWhiteSpace(row.Mascot))
                    {
                        invalidRows ++;
                        _logger.LogWarning("Skipping row {Line}: Team Name or Mascot Name not valid", line);
                        continue;
                    }
                    
                    var team = new Team
                    {
                         Rank = row.Rank,
                         Name = row.Name?.Trim() ?? "",
                         Mascot = row.Mascot?.Trim() ?? "",
                         Wins = DataValidations.ParseIntOrDefault(row.Wins),
                         Losses = DataValidations.ParseIntOrDefault(row.Losses),
                         Ties = DataValidations.ParseIntOrDefault(row.Ties),
                         Games = DataValidations.ParseIntOrDefault(row.Games),
                         WinsPercentage = DataValidations.ParseDecimalOrDefault(row.WinsPercentage)
                    };

                    if (string.IsNullOrWhiteSpace(row.DateOfLastWin) || DataValidations.IsValidDate(row.DateOfLastWin))
                    {
                        team.DateOfLastWin = DateTime.Parse(row.DateOfLastWin);
                    }

                    await _dbContext.Teams.AddAsync(team, ct);
                    validRows++;
                }
                catch (Exception ex)
                {
                     invalidRows++;
                     _logger.LogWarning(ex, "Skipping row {Line}: {Message}", line, ex.Message);
                }
            }
            await  _dbContext.SaveChangesAsync(ct);
        }
    }
}

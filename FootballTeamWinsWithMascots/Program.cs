using FootballTeamWinsWithMascots.Infrastructure.DbContexts;
using FootballTeamWinsWithMascots.Infrastructure.Migrations.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database configuration
builder.Services.AddDbContext<FootballTeamDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:SQLiteDefault"]),
    ServiceLifetime.Scoped);

// Dependency Injection mapps
builder.Services.AddScoped<ICsvTeamsSeeder, CsvTeamsSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Create a cancellation token for database seed operations
using var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

//Migration at startup
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<FootballTeamDbContext>();
        dbContext.Database.Migrate();

        //Seed database from CSV file
        var seeder = scope.ServiceProvider.GetRequiredService<ICsvTeamsSeeder>();
        await seeder.SeedData(cancellationToken);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

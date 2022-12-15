using FootballScout.Data;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Teams;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<ILeaguesRepository, LeaguesRepository>();
builder.Services.AddTransient<ITeamsRepository, TeamsRepository>();
builder.Services.AddTransient<IPlayersRepository, PlayersRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using FootballScout.Data;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Mentals;
using FootballScout.Data.Repositories.Physicals;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Teams;
using FootballScout.Data.Repositories.Technicals;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<ILeaguesRepository, LeaguesRepository>();
builder.Services.AddTransient<ITeamsRepository, TeamsRepository>();
builder.Services.AddTransient<IPlayersRepository, PlayersRepository>();
builder.Services.AddTransient<ITechnicalsRepository, TechnicalsRepository>();
builder.Services.AddTransient<IMentalsRepository, MentalsRepository>();
builder.Services.AddTransient<IPhysicalsRepository, PhysicalsRepository>();
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

using System.Text;
using FootballScout.Authentication;
using FootballScout.Data;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.GoalKeeping;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.ListedPlayers;
using FootballScout.Data.Repositories.Mentals;
using FootballScout.Data.Repositories.Physicals;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.RestUsers;
using FootballScout.Data.Repositories.ShortLists;
using FootballScout.Data.Repositories.Teams;
using FootballScout.Data.Repositories.Technicals;
using FootballScout.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity<RestUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters.ValidAudience = builder.Configuration.GetSection("JWT:ValidAudience").Value;
        options.TokenValidationParameters.ValidIssuer = builder.Configuration.GetSection("JWT:ValidIssuer").Value;
        options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value));
    });
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("NewFmDb")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<ILeaguesRepository, LeaguesRepository>();
builder.Services.AddTransient<ITeamsRepository, TeamsRepository>();
builder.Services.AddTransient<IPlayersRepository, PlayersRepository>();
builder.Services.AddTransient<ITechnicalsRepository, TechnicalsRepository>();
builder.Services.AddTransient<IMentalsRepository, MentalsRepository>();
builder.Services.AddTransient<IPhysicalsRepository, PhysicalsRepository>();
builder.Services.AddTransient<IGoalKeepingRepository, GoalKeepingRepository>();
builder.Services.AddTransient<IShortListsRepository, ShortListsRepository>();
builder.Services.AddTransient<IRestUsersRepository, RestUsersRepository>();
builder.Services.AddTransient<IListedPlayersRepository, ListedPlayersRepository>();
builder.Services.AddTransient<ITokenManager, TokenManager>();
builder.Services.AddTransient<DatabaseSeeder, DatabaseSeeder>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// seeding
using var scope = app.Services.CreateScope();
var dbSeeder = (DatabaseSeeder)scope.ServiceProvider.GetService(typeof(DatabaseSeeder));
await dbSeeder.SeedAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

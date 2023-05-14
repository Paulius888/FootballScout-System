using AutoMapper;
using FluentAssertions;
using FootballScout.Controllers;
using FootballScout.Data;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.GoalKeeping;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Mentals;
using FootballScout.Data.Repositories.Physicals;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Teams;
using FootballScout.Data.Repositories.Technicals;
using FootballScout.Services;
using FootballScout.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    public class AllPlayerControllerTests
    {
        private readonly Mock<IPlayersRepository> playersRepositoryStub = new();
        private readonly Mock<ITeamsRepository> teamsRepositoryStub = new();
        private readonly Mock<ILeaguesRepository> leagueRepositoryStub = new();
        private readonly Mock<ITechnicalsRepository> technicalRepositoryStub = new();
        private readonly Mock<IMentalsRepository> mentalRepositoryStub = new();
        private readonly Mock<IPhysicalsRepository> physicalRepositoryStub = new();
        private readonly Mock<IGoalKeepingRepository> goalkeepingRepositoryStub = new();
        private readonly Mock<IUriService> uriStub = new();
        private readonly Random random = new();

        [Fact]
        public async Task Get_WithUnexistingId_ReturnsNotFound()
        {
            playersRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((Player)null);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new AllPlayersController(playersRepositoryStub.Object,teamsRepositoryStub.Object, mapper,
                leagueRepositoryStub.Object, uriStub.Object, technicalRepositoryStub.Object, mentalRepositoryStub.Object,
                physicalRepositoryStub.Object, goalkeepingRepositoryStub.Object);

            var result = await controller.Get(random.Next(10));

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Get_WithExistingId_ReturnsExpectedPlayer()
        {
            var expectedItem = CreateRandomPlayer();

            playersRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new AllPlayersController(playersRepositoryStub.Object, teamsRepositoryStub.Object, mapper,
                leagueRepositoryStub.Object, uriStub.Object, technicalRepositoryStub.Object, mentalRepositoryStub.Object,
                physicalRepositoryStub.Object, goalkeepingRepositoryStub.Object);

            var result = await controller.Get(random.Next(10));

            var resultObject = GetObjectResultContent<Response<PlayerDto>>(result);

            resultObject.Data.Should().BeEquivalentTo(expectedItem, options => options.ComparingByMembers<PlayerDto>().ExcludingMissingMembers());
        }

        private Player CreateRandomPlayer()
        {
            return new()
            {
                Id = random.Next(10),
                Name = Guid.NewGuid().ToString(),
                Age = random.Next(10),
                Contract = new DateOnly(2024, 1, 1),
                Wage = random.Next(10),
                Price = random.Next(10),
                CurrentAbility = random.Next(10),
                PotentialAbility = random.Next(10),
                IsGoalKeeper = true,
                IsEuCitizen = true,
                Personality = "personality",
                Role = new[] { "DC" },
                League_Name = "Premier League",
                LeagueId = 1,
                Team_Name = "Liverpool",
                TeamId = 1,
                Team = new Team
                {
                    Id = random.Next(10),
                    Name = Guid.NewGuid().ToString(),
                    Training_Facilities = Guid.NewGuid().ToString(),
                    Youth_Facilities = Guid.NewGuid().ToString(),
                    League_Name = Guid.NewGuid().ToString(),
                    LeagueId = random.Next(10),
                    League = new League
                    {
                        Id = random.Next(10),
                        Name = Guid.NewGuid().ToString(),
                        Nation = Guid.NewGuid().ToString()
                    }
                },
                Technical = new Technical(),
                Mental = new Mental(),
                Physical = new Physical(),
                Goalkeeping = new Goalkeeping()
            };
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}

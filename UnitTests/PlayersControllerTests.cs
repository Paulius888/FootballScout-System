using AutoMapper;
using FluentAssertions;
using FootballScout.Controllers;
using FootballScout.Data;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Teams;
using FootballScout.Services;
using FootballScout.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    public class PlayersControllerTests
    {
        private readonly Mock<IPlayersRepository> playersRepositoryStub = new();
        private readonly Mock<ITeamsRepository> teamsRepositoryStub = new();
        private readonly Mock<ILeaguesRepository> leagueRepositoryStub = new();
        private readonly Mock<IUriService> uriStub = new();
        private readonly Random random = new();

        [Fact]
        public async Task Post_WithPlayerToCreate_ReturnsCreatedItem()
        {
            var date = new DateOnly(2024, 1, 1);
            var arr = new[] { "DC" };
            var playerToCreate = new CreatePlayerDto("Test", 1, date, 1, 1,1,1,true,true,"person", arr);

            var expectedItem1 = CreateRandomTeam();

            teamsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem1);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new PlayersController(playersRepositoryStub.Object, teamsRepositoryStub.Object, mapper, leagueRepositoryStub.Object, uriStub.Object);

            var result = await controller.Add(It.IsAny<int>(), It.IsAny<int>(), playerToCreate);

            var resultObject = GetObjectResultContent<Response<PlayerDto>>(result);

            var createdItem = resultObject.Data as PlayerDto;
            playerToCreate.Should().BeEquivalentTo(createdItem, options => options.ComparingByMembers<PlayerDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Put_WithTeamToUpdate_ReturnsUpdatedItem()
        {
            var expectedItem = CreateRandomPlayer();

            var expectedItem1 = CreateRandomTeam();

            teamsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem1);

            playersRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new PlayersController(playersRepositoryStub.Object, teamsRepositoryStub.Object, mapper, leagueRepositoryStub.Object, uriStub.Object);

            var PlayerId = expectedItem.Id;

            var LeagueId = expectedItem.LeagueId;

            var TeamId = expectedItem1.Id;

            var itemToUpdate = new UpdatePlayerDto("Test1", 1, expectedItem.Contract, 1,1,1,1,true,false,"personality", expectedItem.Role);

            var result = await controller.Put(LeagueId, TeamId, PlayerId, itemToUpdate);

            var resultObject = GetObjectResultContent<Response<PlayerDto>>(result);

            var updatedItem = resultObject.Data as PlayerDto;
            itemToUpdate.Should().BeEquivalentTo(updatedItem, options => options.ComparingByMembers<PlayerDto>().ExcludingMissingMembers());

        }

        [Fact]
        public async Task Delete_WithPlayerToDelete_ReturnsNoContent()
        {
            var expectedItem = CreateRandomPlayer();

            var expectedItem1 = CreateRandomTeam();

            teamsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem1);

            playersRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new PlayersController(playersRepositoryStub.Object, teamsRepositoryStub.Object, mapper, leagueRepositoryStub.Object, uriStub.Object);

            var result = await controller.Delete(expectedItem1.Id, expectedItem.Id);

            result.Should().BeOfType<NoContentResult>();

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

        private Team CreateRandomTeam()
        {
            return new()
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
            };
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }

    }
}

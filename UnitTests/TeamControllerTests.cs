using AutoMapper;
using FluentAssertions;
using FootballScout.Controllers;
using FootballScout.Data;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Teams;
using FootballScout.Services;
using FootballScout.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    public class TeamControllerTests
    {
        private readonly Mock<ITeamsRepository> teamsRepositoryStub = new();
        private readonly Mock<ILeaguesRepository> leagueRepositoryStub = new();
        private readonly Mock<IUriService> uriStub = new();
        private readonly Random random = new();

        [Fact]
        public async Task Get_WithUnexistingId_ReturnsNotFound()
        {
            teamsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((Team)null);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new TeamsController(teamsRepositoryStub.Object,  mapper, leagueRepositoryStub.Object, uriStub.Object);

            var result = await controller.Get(random.Next(10), random.Next(10));

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Get_WithExistingId_ReturnsExpectedTeam()
        {
            var expectedItem = CreateRandomTeam();

            teamsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new TeamsController(teamsRepositoryStub.Object, mapper, leagueRepositoryStub.Object, uriStub.Object);

            var result = await controller.Get(random.Next(10), random.Next(10));

            var resultObject = GetObjectResultContent<Response<TeamDto>>(result);

            resultObject.Data.Should().BeEquivalentTo(expectedItem, options => options.ComparingByMembers<TeamDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Post_WithTeamToCreate_ReturnsCreatedItem()
        {
            var teamToCreate = new CreateTeamDto("Test", "Test", "Test");

            var expectedItem1 = CreateRandomLeague();

            leagueRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem1);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new TeamsController(teamsRepositoryStub.Object, mapper, leagueRepositoryStub.Object, uriStub.Object);

            var result = await controller.Add(It.IsAny<int>(), teamToCreate);

            var resultObject = GetObjectResultContent<Response<TeamDto>>(result);

            var createdItem = resultObject.Data as TeamDto;
            teamToCreate.Should().BeEquivalentTo(createdItem, options => options.ComparingByMembers<TeamDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Put_WithTeamToUpdate_ReturnsUpdatedItem()
        {
            var expectedItem = CreateRandomTeam();

            var expectedItem1 = CreateRandomLeague();

            teamsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem);

            leagueRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem1);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new TeamsController(teamsRepositoryStub.Object, mapper, leagueRepositoryStub.Object, uriStub.Object);

            var LeagueId = expectedItem1.Id;

            var TeamId = expectedItem.Id;

            var itemToUpdate = new UpdateTeamDto("Test1", "Test1", "Test1");

            var result = await controller.Put(LeagueId, TeamId,itemToUpdate);

            var resultObject = GetObjectResultContent<Response<TeamDto>>(result);

            var updatedItem = resultObject.Data as TeamDto;
            itemToUpdate.Should().BeEquivalentTo(updatedItem, options => options.ComparingByMembers<TeamDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Delete_WithTeamToDelete_ReturnsNoContent()
        {
            var expectedItem = CreateRandomTeam();

            var expectedItem1 = CreateRandomLeague();

            teamsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new TeamsController(teamsRepositoryStub.Object, mapper, leagueRepositoryStub.Object, uriStub.Object);

            var result = await controller.Delete(expectedItem1.Id, expectedItem.Id);

            result.Should().BeOfType<NoContentResult>();
        }

        private League CreateRandomLeague()
        {
            return new()
            {
                Id = random.Next(10),
                Name = Guid.NewGuid().ToString(),
                Nation = Guid.NewGuid().ToString()
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

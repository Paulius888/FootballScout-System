using AutoMapper;
using FluentAssertions;
using FootballScout.Controllers;
using FootballScout.Data;
using FootballScout.Data.Dtos.Leagues;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Services;
using FootballScout.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    public class LeaguesControllerTests
    {
        private readonly Mock<ILeaguesRepository> leagueRepositoryStub = new();
        private readonly Mock<IUriService> uriStub = new();
        private readonly Random random = new();

        [Fact]
        public async Task Get_WithUnexistingId_ReturnsNotFound()
        {
            leagueRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((League)null);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new LeaguesController(leagueRepositoryStub.Object, mapper, uriStub.Object);

            var result = await controller.Get(random.Next(10));

            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Get_WithExistingId_ReturnsExpectedLeague()
        {
            var expectedItem = CreateRandomLeague();

            leagueRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new LeaguesController(leagueRepositoryStub.Object, mapper, uriStub.Object);

            var result = await controller.Get(random.Next(10));

            var resultObject = GetObjectResultContent<Response<LeagueDto>>(result);

            resultObject.Data.Should().BeEquivalentTo(expectedItem, options => options.ComparingByMembers<League>());
        }

        [Fact]
        public async Task Post_WithLeagueToCreate_ReturnsCreatedItem()
        {
            var leagueToCreate = new CreateLeagueDto("Test", "Test");

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new LeaguesController(leagueRepositoryStub.Object, mapper, uriStub.Object);

            var result = await controller.Post(leagueToCreate);

            var resultObject = GetObjectResultContent<Response<LeagueDto>>(result);

            var createdItem = resultObject.Data as LeagueDto;
            leagueToCreate.Should().BeEquivalentTo(createdItem, options => options.ComparingByMembers<LeagueDto>().ExcludingMissingMembers());

        }

        [Fact]
        public async Task Put_WithLeagueToUpdate_ReturnsUpdatedItem()
        {
            var expectedItem = CreateRandomLeague();

            leagueRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new LeaguesController(leagueRepositoryStub.Object, mapper, uriStub.Object);

            var LeagueId = expectedItem.Id;

            var itemToUpdate = new UpdateLeagueDto("Test1");

            var result = await controller.Put(LeagueId, itemToUpdate);

            var resultObject = GetObjectResultContent<Response<LeagueDto>>(result);

            var updatedItem = resultObject.Data as LeagueDto;
            itemToUpdate.Should().BeEquivalentTo(updatedItem, options => options.ComparingByMembers<LeagueDto>().ExcludingMissingMembers());

        }

        [Fact]
        public async Task Delete_WithLeagueToDelete_ReturnsNoContent()
        {
            var expectedItem = CreateRandomLeague();

            leagueRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new LeaguesController(leagueRepositoryStub.Object, mapper, uriStub.Object);

            var result = await controller.Delete(expectedItem.Id);

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

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
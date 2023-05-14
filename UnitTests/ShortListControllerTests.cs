using AutoMapper;
using FluentAssertions;
using FootballScout.Controllers;
using FootballScout.Data;
using FootballScout.Data.Dtos.ShortList;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.RestUsers;
using FootballScout.Data.Repositories.ShortLists;
using FootballScout.Services;
using FootballScout.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    public class ShortListControllerTests
    {
        private readonly Mock<IShortListsRepository> shortListRepositoryStub = new();
        private readonly Mock<IRestUsersRepository> restUsersRepositoryStub = new();
        private readonly Mock<IUriService> uriStub = new();
        private readonly Random random = new();

        [Fact]
        public async Task Post_WithShortListToCreate_ReturnsCreatedItem()
        {
            var shortListToCreate = new CreateShortListDto("Test");

            var expectedItem = CreateRandomUser();

            restUsersRepositoryStub.Setup(repo => repo.Get(It.IsAny<string>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new ShortListController(mapper, shortListRepositoryStub.Object, restUsersRepositoryStub.Object, uriStub.Object);

            var result = await controller.Add(It.IsAny<string>(), shortListToCreate);

            var resultObject = GetObjectResultContent<Response<ShortListDto>>(result);

            var createdItem = resultObject.Data as ShortListDto;
            shortListToCreate.Should().BeEquivalentTo(createdItem, options => options.ComparingByMembers<ShortListDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Put_WithShortListToUpdate_ReturnsUpdatedItem()
        {
            var expectedItem = CreateRandomShortList();

            shortListRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new ShortListController(mapper, shortListRepositoryStub.Object, restUsersRepositoryStub.Object, uriStub.Object);

            var shortListId = expectedItem.Id;

            var itemToUpdate = new UpdateShortListDto("Test1");

            var result = await controller.Update(shortListId, itemToUpdate);

            var resultObject = GetObjectResultContent<Response<ShortListDto>>(result);

            var updatedItem = resultObject.Data as ShortListDto;
            itemToUpdate.Should().BeEquivalentTo(updatedItem, options => options.ComparingByMembers<ShortListDto>().ExcludingMissingMembers());

        }

        [Fact]
        public async Task Delete_WithShortListToDelete_ReturnsNoContent()
        {
            var expectedItem = CreateRandomShortList();

            shortListRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new ShortListController(mapper, shortListRepositoryStub.Object, restUsersRepositoryStub.Object, uriStub.Object);

            var result = await controller.Delete(expectedItem.Id);

            result.Should().BeOfType<NoContentResult>();

        }

        private ShortList CreateRandomShortList()
        {
            return new()
            {
                Id = 1,
                User = new RestUser(),
                UserId = "UserId",
                Name = "Name"
            };
        }

        private RestUser CreateRandomUser()
        {
            return new()
            {
                Id = "id",
                UserName = "Test",
                NormalizedUserName = "Test",
                Email = "Test",
                NormalizedEmail = "Test",
                EmailConfirmed = true,
                PasswordHash = "Test",
                SecurityStamp = "Test",
                ConcurrencyStamp = "Test",
                PhoneNumber = "Test",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = new DateTime(2024, 02, 29, 12, 43, 0, DateTimeKind.Utc),
                LockoutEnabled = true,
                AccessFailedCount = 1
            };
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}

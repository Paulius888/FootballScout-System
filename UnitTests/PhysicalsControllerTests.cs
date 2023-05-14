using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FootballScout.Controllers;
using FootballScout.Data;
using FootballScout.Data.Dtos.Physicals;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Dtos.Technicals;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Physicals;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Technicals;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    public class PhysicalsControllerTests
    {
        private readonly Mock<IPlayersRepository> playersRepositoryStub = new();
        private readonly Mock<IPhysicalsRepository> physicalsRepositoryStub = new();
        private readonly Random random = new();

        [Fact]
        public async Task Post_WithPhysicalsToCreate_ReturnsCreatedItem()
        {
            var physicalsToCreate = new CreatePhysicalDto(1, 1, 1, 1, 1, 1, 1, 1);

            var expectedItem = CreateRandomPlayer();

            playersRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new PhysicalsController(playersRepositoryStub.Object, mapper, physicalsRepositoryStub.Object);

            var result = await controller.Add(expectedItem.LeagueId, expectedItem.TeamId, expectedItem.Id, physicalsToCreate);

            var resultObject = GetObjectResultContent<PhysicalDto>(result);

            physicalsToCreate.Should().BeEquivalentTo(resultObject, options => options.ComparingByMembers<PhysicalDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Put_WithPhysicalToUpdate_ReturnsUpdatedItem()
        {
            var expectedItem = createPhysicals();

            var expectedItem1 = CreateRandomPlayer();

            physicalsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem);

            playersRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem1);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new PhysicalsController(playersRepositoryStub.Object, mapper, physicalsRepositoryStub.Object);

            var playerId = expectedItem1.Id;

            var mentalId = expectedItem.Id;

            var itemToUpdate = new UpdatePhysicalDto(1, 1, 1, 1, 1, 1, 1, 1);

            var result = await controller.Put(expectedItem1.LeagueId, expectedItem1.TeamId, playerId, mentalId, itemToUpdate);

            var resultObject = GetObjectResultContent<PhysicalDto>(result);

            itemToUpdate.Should().BeEquivalentTo(resultObject, options => options.ComparingByMembers<PhysicalDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Delete_WithPhysicalToDelete_ReturnsNoContent()
        {
            var expectedItem = createPhysicals();

            var expectedItem1 = CreateRandomPlayer();

            physicalsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new PhysicalsController(playersRepositoryStub.Object, mapper, physicalsRepositoryStub.Object);

            var result = await controller.Delete(expectedItem1.Id, expectedItem.Id);

            result.Should().BeOfType<NoContentResult>();
        }

        private Physical createPhysicals()
        {
            return new()
            {
                Id = random.Next(10),
                Acceleration = random.Next(10),
                Agility = random.Next(10),
                Balance = random.Next(10),
                JumpingReach = random.Next(10),
                NaturalFitness = random.Next(10),
                Pace = random.Next(10),
                Stamina = random.Next(10),
                Strength = random.Next(10),
                PlayerId = random.Next(10),
                Player = new Player(),
                FieldStats = new FieldStats(),
                GoalStats = new GoalStats()
            };
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

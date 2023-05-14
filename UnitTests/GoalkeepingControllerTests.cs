using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FootballScout.Controllers;
using FootballScout.Data;
using FootballScout.Data.Dtos.Goalkeeping;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Dtos.Technicals;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.GoalKeeping;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Technicals;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    public class GoalkeepingControllerTests
    {
        private readonly Mock<IPlayersRepository> playersRepositoryStub = new();
        private readonly Mock<IGoalKeepingRepository> goalkeepingRepositoryStub = new();
        private readonly Random random = new();

        [Fact]
        public async Task Post_WithTechnicalsToCreate_ReturnsCreatedItem()
        {
            var technicalsToCreate = new CreateGoalkeepingDto(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            var expectedItem = CreateRandomPlayer();

            playersRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new GoalkeepingController(playersRepositoryStub.Object, mapper, goalkeepingRepositoryStub.Object);

            var result = await controller.Add(expectedItem.LeagueId, expectedItem.TeamId, expectedItem.Id, technicalsToCreate);

            var resultObject = GetObjectResultContent<GoalkeepingDto>(result);

            technicalsToCreate.Should().BeEquivalentTo(resultObject, options => options.ComparingByMembers<GoalkeepingDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Put_WithTechnicalToUpdate_ReturnsUpdatedItem()
        {
            var expectedItem = createGoalkeeping();

            var expectedItem1 = CreateRandomPlayer();

            goalkeepingRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem);

            playersRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem1);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new GoalkeepingController(playersRepositoryStub.Object, mapper, goalkeepingRepositoryStub.Object);

            var playerId = expectedItem1.Id;

            var goalkeepingId = expectedItem.Id;

            var itemToUpdate = new UpdateGoalkeepingDto(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            var result = await controller.Put(expectedItem1.LeagueId, expectedItem1.TeamId, playerId, goalkeepingId, itemToUpdate);

            var resultObject = GetObjectResultContent<GoalkeepingDto>(result);

            itemToUpdate.Should().BeEquivalentTo(resultObject, options => options.ComparingByMembers<GoalkeepingDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Delete_WithTechnicalToDelete_ReturnsNoContent()
        {
            var expectedItem = createGoalkeeping();

            var expectedItem1 = CreateRandomPlayer();

            goalkeepingRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new GoalkeepingController(playersRepositoryStub.Object, mapper, goalkeepingRepositoryStub.Object);

            var result = await controller.Delete(expectedItem1.Id, expectedItem.Id);

            result.Should().BeOfType<NoContentResult>();
        }

        private Goalkeeping createGoalkeeping()
        {
            return new()
            {
                Id = random.Next(10),
                AerialReach = random.Next(10),
                CommandOfArea = random.Next(10),
                Communication = random.Next(10),
                Eccentricity = random.Next(10),
                FirstTouch = random.Next(10),
                Handling = random.Next(10),
                Kicking = random.Next(10),
                OneOnOnes = random.Next(10),
                Passing = random.Next(10),
                Punching = random.Next(10),
                Reflexes = random.Next(10),
                RushingOut = random.Next(10),
                Throwing = random.Next(10),
                PlayerId = random.Next(10),
                Player = new Player(),
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

using AutoMapper;
using FluentAssertions;
using FootballScout.Controllers;
using FootballScout.Data;
using FootballScout.Data.Dtos.Mentals;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Mentals;
using FootballScout.Data.Repositories.Players;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    public class MentalsControllerTest
    {
        private readonly Mock<IPlayersRepository> playersRepositoryStub = new();
        private readonly Mock<IMentalsRepository> mentalsRepositoryStub = new();
        private readonly Random random = new();

        [Fact]
        public async Task Post_WithMentalsToCreate_ReturnsCreatedItem()
        {
            var mentalsToCreate = new CreateMentalDto(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            var expectedItem = CreateRandomPlayer();

            playersRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new MentalsController(playersRepositoryStub.Object, mapper, mentalsRepositoryStub.Object);

            var result = await controller.Add(expectedItem.LeagueId, expectedItem.TeamId, expectedItem.Id, mentalsToCreate);

            var resultObject = GetObjectResultContent<MentalDto>(result);

            mentalsToCreate.Should().BeEquivalentTo(resultObject, options => options.ComparingByMembers<MentalDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Put_WithMentalToUpdate_ReturnsUpdatedItem()
        {
            var expectedItem = createMentals();

            var expectedItem1 = CreateRandomPlayer();

            mentalsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem);

            playersRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(expectedItem1);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new MentalsController(playersRepositoryStub.Object, mapper, mentalsRepositoryStub.Object);

            var playerId = expectedItem1.Id;

            var technicalId = expectedItem.Id;

            var itemToUpdate = new UpdateMentalDto(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            var result = await controller.Put(expectedItem1.LeagueId, expectedItem1.TeamId, playerId, technicalId, itemToUpdate);

            var resultObject = GetObjectResultContent<MentalDto>(result);

            itemToUpdate.Should().BeEquivalentTo(resultObject, options => options.ComparingByMembers<MentalDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task Delete_WithMentalToDelete_ReturnsNoContent()
        {
            var expectedItem = createMentals();

            var expectedItem1 = CreateRandomPlayer();

            mentalsRepositoryStub.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RestProfile());
            });
            var mapper = config.CreateMapper();

            var controller = new MentalsController(playersRepositoryStub.Object, mapper, mentalsRepositoryStub.Object);

            var result = await controller.Delete(expectedItem1.Id, expectedItem.Id);

            result.Should().BeOfType<NoContentResult>();
        }

        private Mental createMentals()
        {
            return new()
            {
                Id = random.Next(10),
                Aggression = random.Next(10),
                Anticipation = random.Next(10),
                Bravery = random.Next(10),
                Composure = random.Next(10),
                Concentration = random.Next(10),
                Decisions = random.Next(10),
                Determination = random.Next(10),
                Flair = random.Next(10),
                Leadership = random.Next(10),
                OffTheBall = random.Next(10),
                Positioning = random.Next(10),
                Teamwork = random.Next(10),
                Vision = random.Next(10),
                WorkRate = random.Next(10),
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

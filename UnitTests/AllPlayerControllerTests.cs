using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FootballScout.Controllers;
using FootballScout.Data;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Dtos.Teams;
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

        //[Fact]
        //public async Task GetFieldPlayerStats_WithExistingId_ReturnsExpectedStats()
        //{
        //    var expectedItem1 = new List<Technical> { createTechnicals() };

        //    var expectedItem2 = createMentals();

        //    var expectedItem3 = createPhysicals();

        //    technicalRepositoryStub.Setup(repo => repo.GetAll(It.IsAny<int>())).Returns(expectedItem1);

        //    mentalRepositoryStub.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedItem2);

        //    physicalRepositoryStub.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedItem3);

        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile(new RestProfile());
        //    });
        //    var mapper = config.CreateMapper();

        //    var controller = new AllPlayersController(playersRepositoryStub.Object, teamsRepositoryStub.Object, mapper,
        //        leagueRepositoryStub.Object, uriStub.Object, technicalRepositoryStub.Object, mentalRepositoryStub.Object,
        //        physicalRepositoryStub.Object, goalkeepingRepositoryStub.Object);

        //    var result = await controller.GetFieldPlayerStats(random.Next(10));

        //    //var resultObject = GetObjectResultContent<Response<PlayerDto>>(result);

        //    //result.Result.Should().BeEquivalentTo(expectedItem, options => options.ComparingByMembers<PlayerDto>().ExcludingMissingMembers());
        //}

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

        private Technical createTechnicals()
        {
            return new()
            {
                Id = random.Next(10),
                Corners = random.Next(10),
                Crossing = random.Next(10),
                Dribbling = random.Next(10),
                Finishing = random.Next(10),
                FirstTouch = random.Next(10),
                FreeKickTaking = random.Next(10),
                Heading = random.Next(10),
                LongShots = random.Next(10),
                LongThrows = random.Next(10),
                Marking = random.Next(10),
                Passing = random.Next(10),
                PenaltyTaking = random.Next(10),
                Tackling = random.Next(10),
                Technique = random.Next(10),
                PlayerId = random.Next(10),
                Player = new Player(),
                FieldStats = new FieldStats()
            };
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

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}

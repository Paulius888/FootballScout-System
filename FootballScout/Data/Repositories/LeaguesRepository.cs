using FootballScout.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FootballScout.Data.Repositories
{
    public class LeaguesRepository : ILeaguesRepository
    {
        public async Task<IEnumerable<League>> GetAll()
        {
            return new List<League>
            {
                new League()
                {
                    Name = "Name",
                    Nation = "Nation"
                },
                new League()
                {
                    Name = "Name",
                    Nation = "Nation"
                }
            };
        }

        public async Task<League> Get(int id)
        {
            return new League()
            {
                Name = "Name",
                Nation = "Nation"
            };
        }

        public async Task<League> Create(League league)
        {
            return new League()
            {
                Name = "Name",
                Nation = "Nation"
            };
        }

        public async Task<League> Put(League league)
        {
            return new League()
            {
                Name = "Name",
                Nation = "Nation"
            };
        }

        public async Task Delete(League league)
        {
        }
    }
}

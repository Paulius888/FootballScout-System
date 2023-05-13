using FootballScout.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScout.Data.Repositories.RestUsers
{
    public class RestUsersRepository : IRestUsersRepository
    {
        private readonly DatabaseContext _databaseContext;

        public RestUsersRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<RestUser> Get(string userId)
        {
            return await _databaseContext.RestUser.FirstOrDefaultAsync(o => o.Id == userId);
        }
    }
}

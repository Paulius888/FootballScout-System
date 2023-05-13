using FootballScout.Data.Entities;

namespace FootballScout.Data.Repositories.RestUsers
{
    public interface IRestUsersRepository
    {
        Task<RestUser> Get(string userId);
    }
}
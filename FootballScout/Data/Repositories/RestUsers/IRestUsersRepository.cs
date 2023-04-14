using FootballScout.Data.Dtos.Auth;

namespace FootballScout.Data.Repositories.RestUsers
{
    public interface IRestUsersRepository
    {
        Task<RestUser> Get(string userId);
    }
}
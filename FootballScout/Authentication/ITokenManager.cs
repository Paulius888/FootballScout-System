using FootballScout.Data.Entities;

namespace FootballScout.Authentication
{
    public interface ITokenManager
    {
        Task<string> CreateAccessTokenAsync(RestUser user);
    }
}
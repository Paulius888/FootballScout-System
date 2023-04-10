using FootballScout.Data.Dtos.Auth;

namespace FootballScout.Authentication
{
    public interface ITokenManager
    {
        Task<string> CreateAccessTokenAsync(RestUser user);
    }
}
using FootballScout.Filter;

namespace FootballScout.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}

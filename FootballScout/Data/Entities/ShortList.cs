using FootballScout.Data.Dtos.Auth;

namespace FootballScout.Data.Entities
{
    public class ShortList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public RestUser User { get; set; }
    }
}

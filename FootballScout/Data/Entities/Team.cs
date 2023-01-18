namespace FootballScout.Data.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Training_Facilities { get; set; }
        public string Youth_Facilities { get; set; }
        public string League_Name { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
    }
}
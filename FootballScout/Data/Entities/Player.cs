namespace FootballScout.Data.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateOnly Contract { get; set; }
        public int Wage { get; set; }
        public int Price { get; set; }
        public int CurrentAbility { get; set; }
        public int PotentialAbility { get; set; }
        public bool IsGoalKeeper { get; set; }
        public bool IsEuCitizen { get; set; }
        public string Personality { get; set; } 
        public string[] Role { get;set; }
        public int LeagueId { get; set; }
        public string Team_Name { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public Technical Technical { get; set; }
        public Mental Mental { get; set; }
        public Physical Physical { get; set; }
        public Goalkeeping Goalkeeping { get; set; }
    }
}

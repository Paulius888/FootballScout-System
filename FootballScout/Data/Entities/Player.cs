namespace FootballScout.Data.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Contract { get; set; }
        public int Wage { get; set; }
        public int Price { get; set; }
        public int CurrentAbility { get; set; }
        public int PotentialAbility { get; set; }
        public bool IsGoalKeeper { get; set; }
        public bool IsEuCitizen { get; set; }
        public PersonalityEnum Personality { get; set; } 
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public Technical Technical { get; set; }
        public Mental Mental { get; set; }
    }
}

namespace FootballScout.Data.Entities
{
    public class Goalkeeping
    {
        public int Id { get; set; }
        public int AerialReach { get; set; }
        public int CommandOfArea { get; set; }
        public int Communication { get; set; }
        public int Eccentricity { get; set; }
        public int FirstTouch { get; set; }
        public int Handling { get; set; }
        public int Kicking { get; set; }
        public int OneOnOnes { get; set; }
        public int Passing { get; set; }
        public int Punching { get; set; }
        public int Reflexes { get; set; }
        public int RushingOut { get; set; }
        public int Throwing { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public GoalStats GoalStats { get; set; }
    }
}
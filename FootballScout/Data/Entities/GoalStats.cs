namespace FootballScout.Data.Entities
{
    public class GoalStats
    {
        public int Id { get; set; }
        public int ShotStoping { get; set; }
        public int Physicals { get; set; }
        public int Speed { get; set; }
        public int Communication { get; set; }
        public int Eccentricity { get; set; }
        public int Distribution { get; set; }
        public int Aerial { get; set; }
        public int Mentals { get; set; }
        public int Overall { get; set; }
        public int GoalkeepingId { get; set; }
        public Goalkeeping Goalkeeping { get; set; }
        public int MentalId { get; set; }
        public Mental Mental { get; set; }
        public int PhysicalId { get; set; }
        public Physical Physical { get; set; }
    }
}

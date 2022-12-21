namespace FootballScout.Data.Entities
{
    public class Physical
    {
        public int Id { get; set; }
        public int Acceleration { get; set; }
        public int Agility { get; set; }
        public int Balance { get; set; }
        public int JumpingReach { get; set; }
        public int NaturalFitness { get; set; }
        public int Pace { get; set; }
        public int Stamina { get; set; }
        public int Strength { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
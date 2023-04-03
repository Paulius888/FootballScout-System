namespace FootballScout.Data.Entities
{
    public class FieldStats
    {
        public int Id { get; set; }
        public int Defending { get; set; }
        public int Physicals { get; set; }
        public int Speed { get; set; }
        public int Vision { get; set; }
        public int Attacking { get; set; }
        public int Technicals { get; set; }
        public int Aerial { get; set; }
        public int Mentals { get; set; }
        public int Overall { get; set; }
        public int TechnicalId { get; set; }
        public Technical Technical { get; set; }
        public int MentalId { get; set; }
        public Mental Mental { get; set; }
        public int PhysicalId { get; set; }
        public Physical Physical { get; set; }

    }
}

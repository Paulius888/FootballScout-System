namespace FootballScout.Data.Entities
{
    public class Technical
    {
        public int Id { get; set; }
        public int Corners { get; set; }
        public int Crossing { get; set; }
        public int Dribbling { get; set; }
        public int Finishing { get; set; }
        public int FirstTouch { get; set; }
        public int FreeKickTaking { get; set; }
        public int Heading { get; set; }
        public int LongShots { get; set; }
        public int LongThrows { get; set; }
        public int Marking { get; set; }
        public int Passing { get; set; }
        public int PenaltyTaking { get; set; }
        public int Tackling { get; set; }
        public int Technique { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}

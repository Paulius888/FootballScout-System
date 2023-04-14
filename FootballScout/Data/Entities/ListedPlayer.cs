namespace FootballScout.Data.Entities
{
    public class ListedPlayer
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int ShortListId { get; set; }
        public ShortList ShortList { get; set; }
    }
}

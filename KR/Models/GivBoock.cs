namespace KR.Models
{
    public class GivBoock
    {
        public int GivBoockId { get; set; }
        public int GivUserId { get; set; }
        public User GivUser { get; set; }
        public int GivBookId { get; set; }
        public Book GivBook { get; set; }
        public DateTime DateGiv { get; set; }
        public DateTime? GivDateReturn { get; set; }
    }
}

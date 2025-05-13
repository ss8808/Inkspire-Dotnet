namespace Inksprie_Backend.Entities
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Book Book { get; set; } = null!;

    }
}

namespace Inksprie_Backend.Dtos
{
    public class BookmarkDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

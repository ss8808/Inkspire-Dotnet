namespace Inksprie_Backend.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string PublisherName { get; set; } = null!;
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; } = null!;
        public DateTime PublicationDate { get; set; }
    }
}

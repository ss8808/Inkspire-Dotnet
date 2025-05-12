namespace Inksprie_Backend.Dtos
{
    public class CreateBookDto
    {
        public string ISBN { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string PublisherName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Format { get; set; } = null!;
        public string Language { get; set; } = null!;
        public DateTime PublicationDate { get; set; }
        public int PageCount { get; set; }
        public bool IsBestseller { get; set; }
        public bool IsAwardWinner { get; set; }
        public bool IsNewRelease { get; set; }
        public bool NewArrival { get; set; }
        public bool CommingSoon { get; set; }
        public bool IsActive { get; set; }
        public int QuantityInStock { get; set; }
        public int ReorderThreshold { get; set; }

        public IFormFile? CoverImageUrl { get; set; }

    }
}

using Microsoft.AspNetCore.Http;

namespace Inksprie_Backend.Dtos
{
    public class UpdateBookDto
    {
        public string? ISBN { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? AuthorName { get; set; }
        public string? PublisherName { get; set; }
        public decimal? Price { get; set; }
        public string? Format { get; set; }
        public string? Language { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int? PageCount { get; set; }
        public bool? IsBestseller { get; set; }
        public bool? IsAwardWinner { get; set; }
        public bool? IsNewRelease { get; set; }
        public bool? NewArrival { get; set; }
        public bool? CommingSoon { get; set; }
        public bool? IsActive { get; set; }
        public int? QuantityInStock { get; set; }
        public int? ReorderThreshold { get; set; }

        public IFormFile? CoverImage { get; set; }
    }
}

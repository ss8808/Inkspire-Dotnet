namespace Inksprie_Backend.Dtos
{
    public class DiscountedBookDto
    {
        // Book details
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string CoverImageUrl { get; set; } = null!;

        // Discount details
        public decimal DiscountPercentage { get; set; }
        public DateTime DiscountStart { get; set; }
        public DateTime DiscountEnd { get; set; }
        public bool OnSale { get; set; }
        public bool IsActive { get; set; }

        // Inventory details
        public int QuantityInStock { get; set; }
    }
}

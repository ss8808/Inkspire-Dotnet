namespace Inksprie_Backend.Dtos
{
    public class DiscountDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; } // Admin
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool OnSale { get; set; }
        public bool IsActive { get; set; }
    }
}

namespace Inksprie_Backend.Dtos
{
    public class CartDto
    {
        public int CartId { get; set; }
        public bool IsPaymentDone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
    }
}

namespace Inksprie_Backend.Dtos
{
    public class CartItemDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
    }
}

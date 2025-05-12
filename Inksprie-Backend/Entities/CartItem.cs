namespace Inksprie_Backend.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
    }
}

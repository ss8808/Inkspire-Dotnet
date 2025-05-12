namespace Inksprie_Backend.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsPaymentDone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}

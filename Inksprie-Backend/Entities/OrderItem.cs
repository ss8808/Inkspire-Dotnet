﻿namespace Inksprie_Backend.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrder { get; set; }
        public decimal Subtotal { get; set; }
        public Order Order { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}

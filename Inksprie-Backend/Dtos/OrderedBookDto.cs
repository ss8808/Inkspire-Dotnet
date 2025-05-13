namespace Inksprie_Backend.Dtos
{
    public class OrderedBookDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CoverImageUrl { get; set; }
    }
}

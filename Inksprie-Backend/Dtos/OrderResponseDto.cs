namespace Inksprie_Backend.Dtos
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public decimal FinalAmount { get; set; }
        public string ClaimCode { get; set; } = null!;
    }
}

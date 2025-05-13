namespace Inksprie_Backend.Dtos
{
    public class OrderHistoryDto
    {
        public int OrderId { get; set; }
        public string ClaimCode { get; set; } = string.Empty;
        public decimal FinalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public string? MemberName { get; set; }
        public string? MemberEmail { get; set; }
        public List<OrderedBookDto> Books { get; set; } = new();
    }
}

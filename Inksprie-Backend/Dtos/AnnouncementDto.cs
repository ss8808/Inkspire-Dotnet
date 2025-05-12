namespace Inksprie_Backend.Dtos
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}

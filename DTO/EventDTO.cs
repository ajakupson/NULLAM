namespace NULLAM_RIK.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DateTimeFormatted { get; set; }
        public string? Place { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}

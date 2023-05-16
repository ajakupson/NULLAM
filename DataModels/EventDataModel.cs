using System.ComponentModel.DataAnnotations;

namespace NULLAM_RIK.DataModels
{
    public class EventDataModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Place { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = false;
        public List<ParticipantDataModel>? Participants { get; set; }


    }
}

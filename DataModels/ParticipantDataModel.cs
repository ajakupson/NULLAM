using System.ComponentModel.DataAnnotations;

namespace NULLAM_RIK.DataModels
{
    public class ParticipantDataModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        [Required]
        public string? PersonalCode { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PaymentType { get; set; }
        public int EventId { get; set; }
        public string? Description { get; set; }
        public int Type { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace NULLAM_RIK.DataModels
{
    public class ParticipantDataModel
    {
        [Key]
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PersonalCode { get; set; }
        public int PayemtType { get; set; }
        public int EventId { get; set; }
    }
}

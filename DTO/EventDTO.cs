using NULLAM_RIK.DataModels;

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
        public List<ParticipantDataModel>? Participants { get; set; }


        public EventDTO() { }

        public EventDTO(EventDataModel edm)
        {
            Id = edm.Id;
            Name = edm.Name;
            Place = edm.Place;
            Description = edm.Description;
            IsActive = edm.IsActive;
            Participants = edm.Participants;
        }
    }
}

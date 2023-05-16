using NULLAM_RIK.DataModels;

namespace NULLAM_RIK.IRepository
{
    public interface IParticipantRepository
    {
        public List<ParticipantDataModel> GetParticipantsByEventId(int eventId);
        public int AddParticipant(ParticipantDataModel participant);
        public bool RemoveParticipant(ParticipantDataModel participant);
        public bool SaveParticipant(ParticipantDataModel participant);
        public ParticipantDataModel GetParticipantById(int id);
    }
}

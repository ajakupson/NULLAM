using NULLAM_RIK.DataModels;

namespace NULLAM_RIK.IRepository
{
    public interface IEventRepository
    {
        public List<EventDataModel> GetEvents();
        public EventDataModel GetEventWithParticipants(int eventId);
        public bool AddEvent(EventDataModel evt);
        public bool RemoveEvent(int eventId);
    }
}

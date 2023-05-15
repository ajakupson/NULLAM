using Microsoft.EntityFrameworkCore;
using NULLAM_RIK.Database;
using NULLAM_RIK.DataModels;
using NULLAM_RIK.IRepository;

namespace NULLAM_RIK.Repositorty
{
    public class EventRepository : IEventRepository
    {
        public EventRepository() {

            using (var context = new DatabaseContext())
            {
                var events = new List<EventDataModel>
                {
                    new EventDataModel
                    {
                        Id = 1,
                        Name = "Aenan commodo",
                        DateTime = DateTime.ParseExact("14.02.2016", "dd.MM.yyyy", null),
                        IsActive = true
                    },
                    new EventDataModel
                    {
                        Id = 2,
                        Name = "Fusce ex dui, finibus eu luctus vel",
                        DateTime = DateTime.ParseExact("18.02.2016", "dd.MM.yyyy", null),
                        IsActive = true
                    },
                    new EventDataModel
                    {
                        Id = 3,
                        Name = "Nunc lobortis metus eu massa viverra ultri iplacerat nibih",
                        DateTime = DateTime.ParseExact("03.03.2016", "dd.MM.yyyy", null),
                        IsActive = true
                    },
                    new EventDataModel
                    {

                        Id= 4,
                        Name = "Integer nec nulla vitae",
                        DateTime = DateTime.ParseExact("22.03.2016", "dd.MM.yyyy", null),
                        IsActive = true
                    },
                    new EventDataModel
                    {
                        Id = 5, 
                        Name = "Aenan commodo",
                        DateTime = DateTime.ParseExact("14.01.2016", "dd.MM.yyyy", null),
                        IsActive = false
                    },
                    new EventDataModel
                    {
                        Id = 6,
                        Name = "Fusce ex dui, finibus eu luctus vel",
                        DateTime = DateTime.ParseExact("18.12.2015", "dd.MM.yyyy", null),
                        IsActive = false
                    },
                    new EventDataModel
                    {
                        Id = 7,
                        Name = "Nunc lobortis metus eu massa viverra ultri iplacerat nibih",
                        DateTime = DateTime.ParseExact("03.12.2015", "dd.MM.yyyy", null),
                        IsActive = false
                    },
                    new EventDataModel
                    {
                        Id = 8,
                        Name = "Integer nec nulla vitae",
                        DateTime = DateTime.ParseExact("22.11.2015", "dd.MM.yyyy", null),
                        IsActive = false
                    },
                    new EventDataModel
                    {
                        Id = 9,
                        Name = "Praesent molestie dapibus lorem",
                        DateTime = DateTime.ParseExact("01.11.2015", "dd.MM.yyyy", null),
                        IsActive = false
                    }
                };

                context.Events.AddRange(events);
                context.SaveChanges();
            }
        }

        public List<EventDataModel> GetEvents()
        {
            using (var context = new DatabaseContext())
            {
                return context.Events.OrderBy(e => e.DateTime).ToList();
            }      
        }

        public EventDataModel GetEventWithParticipants(int eventId)
        {
            using (var context = new DatabaseContext())
            {
                EventDataModel evt = context.Events.FirstOrDefault(e => e.Id == eventId);
                var participants = context.Participants.Where(p => p.EventId == eventId).ToList();
                evt.Participants = participants;

                return evt;
            }
        }

        public bool AddEvent(EventDataModel evt)
        {
            using (var context = new DatabaseContext())
            {
                context.Events.Add(evt);
                context.SaveChanges();
                return true;
            }
        }

        public bool RemoveEvent(int eventId)
        {
            using (var context = new DatabaseContext())
            {
                EventDataModel edm = new EventDataModel() { Id = eventId };
                context.Events.Remove(edm);
                context.SaveChanges();

                return true;
            }
        }
    }
}

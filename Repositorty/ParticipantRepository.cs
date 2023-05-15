using Microsoft.Extensions.Logging;
using NULLAM_RIK.Database;
using NULLAM_RIK.DataModels;
using NULLAM_RIK.IRepository;

namespace NULLAM_RIK.Repositorty
{
    public class ParticipantRepository: IParticipantRepository
    {
        public ParticipantRepository()
        {

            using (var context = new DatabaseContext())
            {
                var participants = new List<ParticipantDataModel>
                {
                    new ParticipantDataModel
                    {
                        Id = 1,
                        Firstname = "Mihkel",
                        Lastname = "Amman",
                        PersonalCode = "37810014694",
                        EventId = 2
                    },
                    new ParticipantDataModel
                    {
                        Id = 2,
                        Firstname = "Nullam",
                        Lastname = "OÜ",
                        PersonalCode = "70000622",
                        EventId = 2
                    },
                    new ParticipantDataModel
                    {
                        Id = 3,
                        Firstname = "Uku",
                        Lastname = "Leele",
                        PersonalCode = "37810014944",
                        EventId = 2
                    }
                };

                context.Participants.AddRange(participants);
                context.SaveChanges();
            }
        }

        public List<ParticipantDataModel> GetParticipantsByEventId(int eventId)
        {
            using (var context = new DatabaseContext())
            {
                return context.Participants.Where(p => p.EventId == eventId).ToList();
            }
        }

        public int AddParticipant(ParticipantDataModel participant)
        {
            using (var context = new DatabaseContext())
            {
                int lastId = 0;
                List<ParticipantDataModel> list = context.Participants.ToList();
                if (list.Count > 0)
                {
                    lastId = list[^1].Id + 1;
                }
                participant.Id = lastId;

                context.Participants.Add(participant);
                context.SaveChanges();

                return lastId;
            }
        }

        public bool RemoveParticipant(ParticipantDataModel participant)
        {
            using (var context = new DatabaseContext())
            {
                context.Participants.Remove(participant);
                context.SaveChanges();
                return true;
            }
        }
    }
}

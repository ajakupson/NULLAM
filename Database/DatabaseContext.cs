using Microsoft.EntityFrameworkCore;
using NULLAM_RIK.DataModels;

namespace NULLAM_RIK.Database
{
    public class DatabaseContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Events");
        }

        public DbSet<EventDataModel> Events { get; set; }
        public DbSet<ParticipantDataModel> Participants { get; set; }
    }
}

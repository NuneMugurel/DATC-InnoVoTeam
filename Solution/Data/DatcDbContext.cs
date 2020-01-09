using Data.Model;
using System.Data.Entity;

namespace Data
{
    public class DatcDbContext : DbContext, IDatcDbContext
    {
        public DatcDbContext() : base("name=DATC")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Voter> Voters { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<SecretQuestion> SecretQuestions { get; set; }

        public DbSet<VotingSession> VotingSessions { get; set; }

        public DbSet<Vote> Votes { get; set; }
    }
}

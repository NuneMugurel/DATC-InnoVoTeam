using Data.Model;
using System.Data.Entity;

namespace Data
{
    public class DatcDbContext : DbContext, IDatcDbContext
    {
        public DatcDbContext() : base("name=DATC")
        {

        }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
    }
}

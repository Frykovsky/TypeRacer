using System.Data.Entity;
using TypeRacerDomain.DBs;

namespace TypeRacerDomain.Concrete
{
    public class EFDBContext: DbContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<UserRace> Races { get; set; }
    }
}

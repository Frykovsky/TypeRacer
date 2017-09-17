using System.Collections.Generic;
using TypeRacerDomain.Abstract;
using TypeRacerDomain.DBs;

namespace TypeRacerDomain.Concrete
{
    public class RacesRepository: IGlobalRacesRepository
    {
        private EFDBContext context = new EFDBContext();
        public IEnumerable<UserRace> Races
        {
            get { return context.Races; }
        }

        public void SaveRace(UserRace race)
        {
            context.Races.Add(race);
            context.SaveChanges();
        }
        public UserRace DeleteRace(int raceId)
        {
            UserRace dbEntry = context.Races.Find(raceId);
            if (dbEntry != null)
            {
                context.Races.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}

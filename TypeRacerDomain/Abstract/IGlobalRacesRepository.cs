using System.Collections.Generic;
using TypeRacerDomain.DBs;

namespace TypeRacerDomain.Abstract
{
    public interface IGlobalRacesRepository
    {
        IEnumerable<UserRace> Races { get;  }
        void SaveRace(UserRace race);
        UserRace DeleteRace(int raceId);
    }
}

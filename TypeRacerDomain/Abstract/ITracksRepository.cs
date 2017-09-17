using System.Collections.Generic;
using TypeRacerDomain.DBs;

namespace TypeRacerDomain.Abstract
{
    public interface ITracksRepository
    {
        IEnumerable<Track> Tracks { get;  }
        void SaveTrack(Track track);
        Track DeleteTrack(int trackId);
    }
}

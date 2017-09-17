using System.Collections.Generic;
using TypeRacerDomain.Abstract;
using TypeRacerDomain.DBs;

namespace TypeRacerDomain.Concrete
{
    public class TracksRepository : ITracksRepository
    {
        private EFDBContext context = new EFDBContext();
        public IEnumerable<Track> Tracks
        {
            get { return context.Tracks; }
        }

        public void SaveTrack(Track track)
        {
            context.Tracks.Add(track);
            context.SaveChanges();
        }
        public Track DeleteTrack(int trackId)
        {
            Track dbEntry = context.Tracks.Find(trackId);
            if (dbEntry != null)
            {
                context.Tracks.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}

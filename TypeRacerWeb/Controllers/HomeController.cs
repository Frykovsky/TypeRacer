using System;
using System.Linq;
using System.Web.Mvc;
using TypeRacerDomain.Abstract;

namespace TypeRacerWeb.Controllers
{
    public class HomeController : Controller
    {
        private ITracksRepository Tracks;
        private IGlobalRacesRepository Races;

        public HomeController(ITracksRepository tracks, IGlobalRacesRepository races)
        {
            Tracks = tracks;
            Races = races;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Race()
        {
	    Random rnd = new Random();
            return View(Tracks.Tracks.ElementAt(rnd.Next(0,Tracks.Tracks.Count())));
        }

        public ActionResult HighScores(int track = 0)
        {
            var result = (track!=0) ? Races.Races.Where(x => x.TrackID == track).OrderByDescending(x=>x.Speed).Take(10) : Races.Races.OrderByDescending(x => x.Speed).Take(10);
            return View(result);
        }
    }
}

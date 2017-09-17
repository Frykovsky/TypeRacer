using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TypeRacerDomain.Abstract;
using TypeRacerDomain.DBs;

namespace TypeRacerWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ITracksRepository Tracks;
        private IGlobalRacesRepository Races;

        public UserController(ITracksRepository tracks, IGlobalRacesRepository races)
        {
            Tracks = tracks;
            Races = races;
        }

        public ActionResult ViewYourRaces()
        {
            return View(Races.Races.Where(x => x.UserName == HttpContext.User.Identity.Name).OrderByDescending(x=>x.RaceGlobalID).Take(10));
        }

        public ActionResult DownloadYourRaces()
        {
            var products = Races.Races.ToList();

            var grid = new GridView();
            grid.DataSource = products.Where(x => x.UserName == HttpContext.User.Identity.Name).Select(x=>new { Date = x.RaceDate, Speed = x.Speed, Mistakes = x.Mistakes });
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            string filename = "attachment; filename ="+ HttpContext.User.Identity.Name + "Races.xls";
            Response.AddHeader("content-disposition", filename);
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("ViewYourRaces");
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult UploadTrack()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public ActionResult UploadTrack(string text)
        {
            if (text.Length < 20)
            {
                TempData["FailMessage"] = string.Format("Your track couldn't be saved, it's too short");
            }
            else
            {
                string newtext = Regex.Replace(text, @"\s+", " ");
                //while (Regex.IsMatch(text, @"\s+$")) text.Remove(text.Length - 1); 
                Track track = new Track();
                if (newtext.EndsWith(" ")) track.Text = newtext.Remove(newtext.Length - 1);
                else track.Text = newtext;
                track.UploadDate = DateTime.Now;
                track.Uploader = HttpContext.User.Identity.Name;
                Tracks.SaveTrack(track);
                TempData["Message"] = string.Format("Track saved successfully");
            }
            
            return RedirectToAction("UploadTrack");
        }

        [HttpPost]
        public void SaveRace(int mistakes, int speed, int trackId)
        {
            UserRace race = new UserRace();
            race.TrackID = trackId;
            race.Mistakes = mistakes;
            race.Speed = speed;
            race.RaceDate = DateTime.Now;
            race.UserName = HttpContext.User.Identity.Name;
            Races.SaveRace(race);    
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteTracks(int trackId)
        {
            Track deletedProduct = Tracks.DeleteTrack(trackId);
            if (deletedProduct != null)
            {
                TempData["Message"] = string.Format("Track was deleted");
            }
            return RedirectToAction("ViewTracks");
        }

        [AllowAnonymous]
        public ActionResult ViewTracks()
        {
            return View(Tracks.Tracks);
        }
    }
}
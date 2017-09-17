using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TypeRacerDomain.DBs
{
    [Table("UsersRaces")]
    public class UserRace
    {
        [Key]
        public int RaceGlobalID { get; set; }

        public string UserName { get; set; }

        public int Speed { get; set; }

        public int Mistakes { get; set; }

        public DateTime RaceDate { get; set; }

        [ForeignKey("Track")]
        public int TrackID { get; set; }

        public virtual Track Track { get; set; }
        
    }
}

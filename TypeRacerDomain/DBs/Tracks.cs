using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TypeRacerDomain.DBs
{
    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int TrackID {get; set; }
        public DateTime UploadDate { get; set; }
        public string Uploader { get; set; }
        public string Text { get; set; }
        public virtual IEnumerable<UserRace> Races { get; set; }
    }
}

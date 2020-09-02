using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Artist
    {
        public Artist()
        {
            Songs = new HashSet<Song>();
        }

        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistSongs { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}

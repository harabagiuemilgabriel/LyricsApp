using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Song
    {
        public Song()
        {
            Lyrics = new HashSet<Lyric>();
        }

        public int SongId { get; set; }
        public string SongName { get; set; }
        public int? Author { get; set; }

        public virtual Artist AuthorNavigation { get; set; }
        public virtual ICollection<Lyric> Lyrics { get; set; }
    }
}

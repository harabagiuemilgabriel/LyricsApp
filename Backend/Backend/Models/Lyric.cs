using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Lyric
    {
        public int LyricsId { get; set; }
        public string Lyrics { get; set; }
        public int? Song { get; set; }

        public virtual Song SongNavigation { get; set; }
    }
}

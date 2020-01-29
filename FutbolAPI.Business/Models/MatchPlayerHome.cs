using System;
using System.Collections.Generic;

namespace FutbolAPI.Business.Models
{
    public partial class MatchPlayerHome
    {
        public int Id { get; set; }
        public int? Idplayer { get; set; }
        public int? Idmatch { get; set; }

        public virtual Match IdmatchNavigation { get; set; }
        public virtual Player IdplayerNavigation { get; set; }
    }
}

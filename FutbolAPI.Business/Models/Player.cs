using System;
using System.Collections.Generic;

namespace FutbolAPI.Business.Models
{
    public partial class Player
    {
        public Player()
        {
            MatchPlayerAway = new HashSet<MatchPlayerAway>();
            MatchPlayerHome = new HashSet<MatchPlayerHome>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public int? YellowCards { get; set; }
        public int? RedCards { get; set; }
        public int? Number { get; set; }
        public int? MinutesPlayed { get; set; }

        public virtual ICollection<MatchPlayerAway> MatchPlayerAway { get; set; }
        public virtual ICollection<MatchPlayerHome> MatchPlayerHome { get; set; }
    }
}

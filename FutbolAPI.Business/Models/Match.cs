using System;
using System.Collections.Generic;

namespace FutbolAPI.Business.Models
{
    public partial class Match
    {
        public Match()
        {
            MatchPlayerAway = new HashSet<MatchPlayerAway>();
            MatchPlayerHome = new HashSet<MatchPlayerHome>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdHomeManager { get; set; }
        public int? IdAwayManager { get; set; }
        public int? Idreferee { get; set; }
        public DateTime? Date { get; set; }

        public virtual Manager IdAwayManagerNavigation { get; set; }
        public virtual Manager IdHomeManagerNavigation { get; set; }
        public virtual Referee IdrefereeNavigation { get; set; }
        public virtual ICollection<MatchPlayerAway> MatchPlayerAway { get; set; }
        public virtual ICollection<MatchPlayerHome> MatchPlayerHome { get; set; }
    }
}

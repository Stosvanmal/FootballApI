using System;
using System.Collections.Generic;

namespace FutbolAPI.Business.Models
{
    public partial class Manager
    {
        public Manager()
        {
            MatchIdAwayManagerNavigation = new HashSet<Match>();
            MatchIdHomeManagerNavigation = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public int? YellowCards { get; set; }
        public int? RedCards { get; set; }

        public virtual ICollection<Match> MatchIdAwayManagerNavigation { get; set; }
        public virtual ICollection<Match> MatchIdHomeManagerNavigation { get; set; }
    }
}

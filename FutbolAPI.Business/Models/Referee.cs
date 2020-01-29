using System;
using System.Collections.Generic;

namespace FutbolAPI.Business.Models
{
    public partial class Referee: IPerson
    {
        public Referee()
        {
            Match = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? MinutesPlayed { get; set; }

        public virtual ICollection<Match> Match { get; set; }
    }
}

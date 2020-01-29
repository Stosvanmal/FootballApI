using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolAPI.Web.ViewModel
{
    public class MatchVMResponse
    {
        public string Name { get; set; }
        public int? HomeManager { get; set; }
        public int? AwayManager { get; set; }
        public int? Referee { get; set; }
        public List<int?> AwayPlayers { get; set; }
        public List<int?> HomePlayers { get; set; }
        public DateTime? Date { get; set; }
    }
}

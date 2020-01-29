using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolAPI.Web.ViewModel
{
    public class ManagerVM
    {
        public string Name { get; set; }
        public string TeamName { get; set; }
        public int? YellowCards { get; set; }
        public int? RedCards { get; set; }
    }
}

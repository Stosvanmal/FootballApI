using FutbolAPI.Business.Models;
using FutbolAPI.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolAPI.Web.Mappers
{
    public static class Mapper
    {
        //Se puede usar el automapper...
        public static Manager ToModel(this ManagerVM vm, Manager m = null)
        {
            if(m == null)
            {
                m = new Manager();
            }
            m.Name = vm.Name;
            m.RedCards = vm.RedCards;
            m.TeamName = vm.TeamName;
            m.YellowCards = vm.YellowCards;
            return m;
        }

        public static Player ToModel(this PlayerVM vm, Player p = null)
        {
            if (p == null)
            {
                p = new Player();
            }
            p.Name = vm.Name;
            p.RedCards = vm.RedCards;
            p.TeamName = vm.TeamName;
            p.Number = vm.Number;
            p.YellowCards = vm.YellowCards;
            return p;
        }

        public static Referee ToModel(this RefereeVM vm, Referee r = null)
        {
            if (r == null)
            {
                r = new Referee();
            }
            r.Name = vm.Name;
            r.MinutesPlayed = vm.MinutesPlayed;
            return r;
        }
        public static ICollection<MatchPlayerAway> ToModelAway(this List<int> vm, int idMatch)
        {
            List<MatchPlayerAway> lstResult = new List<MatchPlayerAway>();
            vm.ForEach(x => lstResult.Add(new MatchPlayerAway { Idplayer = x, Idmatch = idMatch }));
            return lstResult;
        }
        public static ICollection<MatchPlayerHome> ToModelHome(this List<int> vm, int idMatch)
        {
            List<MatchPlayerHome> lstResult = new List<MatchPlayerHome>();
            vm.ForEach(x => lstResult.Add(new MatchPlayerHome { Idplayer = x, Idmatch = idMatch }));
            return lstResult;
        }

        public static Match ToModel(this MatchVMRequest vm, Match m = null)
        {
            if (m == null)
            {
                m = new Match();
            }
            m.Name = vm.Name;
            m.Date = vm.Date;
            m.IdHomeManager = vm.HomeManager;
            m.IdAwayManager = vm.AwayManager;
            m.MatchPlayerAway = vm.AwayPlayers.ToModelAway(m.Id);
            m.MatchPlayerHome = vm.HomePlayers.ToModelHome(m.Id);
            m.Idreferee = vm.Referee;
            return m;
        }

        public static List<CardsVM> ToViewModelLst(this List<Player> p, bool red = false)
        {
            List<CardsVM> result = new List<CardsVM>();
            p.ForEach(x => result.Add(x.ToViewModel(red)));
            return result;
        }

        public static CardsVM ToViewModel(this Player p, bool red = false)
        {
            CardsVM c = new CardsVM();
            c.Id = p.Id;
            c.Name = p.Name;
            c.TeamName = p.TeamName;
            if (!red)
                c.Total = p.YellowCards;
            else c.Total = p.RedCards;
            return c;
        }

        public static List<MinutesVM> ToViewModelLst(this List<Player> p)
        {
            List<MinutesVM> result = new List<MinutesVM>();
            p.ForEach(x => result.Add(x.ToViewModel()));
            return result;
        }

        public static MinutesVM ToViewModel(this Player p)
        {
            MinutesVM m = new MinutesVM();
            m.Id = p.Id;
            m.Name = p.Name;
            m.Total = p.MinutesPlayed;
            return m;
        }


        public static List<int?> ToModelVMHome(this ICollection<MatchPlayerHome> vm)
        {
            List<int?> lstResult = new List<int?>();
            vm.ToList().ForEach(x => lstResult.Add(x.Idplayer));
            return lstResult;
        }
        public static List<int?> ToModelVMAway(this ICollection<MatchPlayerAway> vm)
        {
            List<int?> lstResult = new List<int?>();
            vm.ToList().ForEach(x => lstResult.Add(x.Idplayer));
            return lstResult;
        }
        public static List<MatchVMResponse> ToViewModelLst(this IEnumerable<Match> m)
        {
            List<MatchVMResponse> mVM = new List<MatchVMResponse>();
            m.ToList().ForEach(x => mVM.Add(x.ToViewModel()));
            return mVM;
        }
        public static MatchVMResponse ToViewModel(this Match m)
        {
            MatchVMResponse mVM = new MatchVMResponse();
            mVM.Name = m.Name;
            mVM.HomeManager = m.IdHomeManager;
            mVM.Date = m.Date;
            mVM.AwayManager = m.IdAwayManager;
            mVM.Referee = m.Idreferee;
            mVM.HomePlayers = m.MatchPlayerHome.ToModelVMHome();
            mVM.AwayPlayers = m.MatchPlayerAway.ToModelVMAway();
            return mVM;
        }
    }
}

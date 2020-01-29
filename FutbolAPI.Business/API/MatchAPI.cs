using FutbolAPI.Business.Models;
using FutbolAPI.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolAPI.Business.API
{
    public interface IMatchAPI
    {
        Task<int> Add(Match model);
        Task<Match> Update(Match model);
        Task<bool> Delete(int id);
        Task<Match> GetById(int id);
        Task<IEnumerable<Match>> GetAll();
        Task<IEnumerable<int?>> GetMatchesNow();
        Task<List<IPerson>> GetPlayerNotPlay(List<int?> idmatches);
        Task<List<IPerson>> GetManagerNotPlay(List<int?> idmatches);
    }
    public class MatchAPI: IMatchAPI
    {
        private readonly IRepositoryManager rm;

        public MatchAPI(IRepositoryManager rm)
        {
            this.rm = rm;
        }

        public async Task<int> Add(Match model)
        {
            return (await this.rm.Match.Add(model)).Id;

        }

        public async Task<bool> Delete(int id)
        {
            await DeleteChilds(id);
            return await this.rm.Match.Delete(id);
        }

        public async Task<IEnumerable<Match>> GetAll()
        {
            return await this.rm.Match.GetAll().ToListAsync();
        }

        public async Task<Match> GetById(int id)
        {
            var match = await this.rm.Match.GetByFilter(x=> x.Id == id).Include(x=> x.IdrefereeNavigation)                                                                  
                                                                  .Include(x=> x.IdHomeManagerNavigation)                                                                  
                                                                  .Include(x=> x.IdAwayManagerNavigation).FirstOrDefaultAsync();
            match.MatchPlayerAway = await this.rm.MatchPlayerAway.GetByFilter(x => x.Idmatch == id).ToListAsync();
            match.MatchPlayerHome = await this.rm.MatchPlayerHome.GetByFilter(x => x.Idmatch == id).ToListAsync();
            return match;
        }

        public async Task<List<IPerson>> GetManagerNotPlay(List<int?> idmatches)
        {
            List<IPerson> LstManagers = new List<IPerson>();
            LstManagers.AddRange(await this.rm.Match.GetByFilter(x => idmatches.Contains(x.Id) && ((x.IdAwayManagerNavigation.YellowCards % 5 == 0 || x.IdAwayManagerNavigation.RedCards == 1))).Select(x => x.IdAwayManagerNavigation).ToListAsync());
            LstManagers.AddRange(await this.rm.Match.GetByFilter(x => idmatches.Contains(x.Id) && ((x.IdHomeManagerNavigation.YellowCards % 5 == 0 || x.IdHomeManagerNavigation.RedCards == 1))).Select(x => x.IdHomeManagerNavigation).ToListAsync());
            return LstManagers;
        }

        public async Task<IEnumerable<int?>> GetMatchesNow()
        {
            DateTime ahora = DateTime.Now;
            DateTime cleanAhora = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, ahora.Minute, 0);
            DateTime cincoAtras = cleanAhora.AddMinutes(-5);
#if DEBUG
            return new List<int?> { 1, 2 };

#endif
            return await this.rm.Match.GetByFilter(x => x.Date == cincoAtras).Select(x => (int?)x.Id).ToListAsync();
        }

        public async Task<List<IPerson>> GetPlayerNotPlay(List<int?> idmatches)
        {
            List<IPerson> LstPlayers = new List<IPerson>();
            LstPlayers.AddRange(await this.rm.MatchPlayerAway.GetByFilter(x => idmatches.Contains(x.Idmatch) && ((x.IdplayerNavigation.YellowCards % 5 == 0) || (x.IdplayerNavigation.RedCards == 1))).Include(x=> x.IdplayerNavigation).Select(x=> x.IdplayerNavigation).ToListAsync());
            LstPlayers.AddRange(await this.rm.MatchPlayerHome.GetByFilter(x => idmatches.Contains(x.Idmatch) && ((x.IdplayerNavigation.YellowCards % 5 == 0) || (x.IdplayerNavigation.RedCards == 1))).Include(x => x.IdplayerNavigation).Select(x => x.IdplayerNavigation).ToListAsync());
            return LstPlayers;
        }

        public async Task<Match> Update(Match model)
        {

            await DeleteChilds(model.Id);
            return await this.rm.Match.Update(model);
        }
        private async Task DeleteChilds(int idMatch)
        {
            var PlayerAway = await this.rm.MatchPlayerAway.GetByFilter(x => x.Idmatch == idMatch).ToListAsync();
            var PlayerHome = await this.rm.MatchPlayerHome.GetByFilter(x => x.Idmatch == idMatch).ToListAsync();
            foreach (var item in PlayerHome)
            {
                await this.rm.MatchPlayerHome.Delete(item.Id);
            }
            foreach (var item in PlayerAway)
            {
                await this.rm.MatchPlayerAway.Delete(item.Id);
            }
        }
    }
}

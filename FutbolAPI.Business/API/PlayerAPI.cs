using FutbolAPI.Business.Models;
using FutbolAPI.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FutbolAPI.Business.API
{
    public interface IPlayerAPI
    {
        Task<int> Add(Player model);
        Task<Player> Update(Player model);
        Task<bool> Delete(int id);
        Task<Player> GetById(int id);
        Task<IEnumerable<Player>> GetAll();
        Task<IEnumerable<Player>> GetYellowCardPlayers();
        Task<IEnumerable<Player>> GetRedCardPlayers();
        Task<IEnumerable<Player>> GetMinutesPlayers();
    }
    public class PlayerAPI: IPlayerAPI
    {
        private readonly IRepositoryManager rm;

        public PlayerAPI(IRepositoryManager rm)
        {
            this.rm = rm;
        }

        public async Task<int> Add(Player model)
        {
            return (await this.rm.Player.Add(model)).Id;
        }

        public async Task<bool> Delete(int id)
        {
            return await this.rm.Player.Delete(id);
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            return await this.rm.Player.GetAll().ToListAsync();
        }

        public async Task<Player> GetById(int id)
        {
            return await this.rm.Player.GetById(id);
        }

        public async Task<IEnumerable<Player>> GetYellowCardPlayers()
        {
            return await this.rm.Player.GetByFilter(x => x.YellowCards.HasValue && x.YellowCards > 0).ToListAsync();
        }
        public async Task<IEnumerable<Player>> GetRedCardPlayers()
        {
            return await this.rm.Player.GetByFilter(x => x.RedCards.HasValue && x.RedCards > 0).ToListAsync();
        }
        public async Task<IEnumerable<Player>> GetMinutesPlayers()
        {
            return await this.rm.Player.GetByFilter(x => x.MinutesPlayed.HasValue && x.MinutesPlayed > 0).ToListAsync();
        }

        public async Task<Player> Update(Player model)
        {
            return await this.rm.Player.Update(model);
        }
    }
}

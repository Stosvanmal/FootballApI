using FutbolAPI.Business.Models;
using FutbolAPI.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FutbolAPI.Business.API
{
    public interface IRefereeAPI
    {
        Task<int> Add(Referee model);
        Task<Referee> Update(Referee model);
        Task<bool> Delete(int id);
        Task<Referee> GetById(int id);
        Task<IEnumerable<Referee>> GetAll();
    }
    public class RefereeAPI: IRefereeAPI
    {
        private readonly IRepositoryManager rm;

        public RefereeAPI(IRepositoryManager rm)
        {
            this.rm = rm;
        }

        public async Task<int> Add(Referee model)
        {
            return (await this.rm.Referee.Add(model)).Id;
        }

        public async Task<bool> Delete(int id)
        {
            return await this.rm.Referee.Delete(id);
        }

        public async Task<IEnumerable<Referee>> GetAll()
        {
            return await this.rm.Referee.GetAll().ToListAsync();
        }

        public async Task<Referee> GetById(int id)
        {
            return await this.rm.Referee.GetById(id);
        }

        public async Task<Referee> Update(Referee model)
        {
            return await this.rm.Referee.Update(model);
        }
    }
}

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
    public interface IManagerAPI
    {
        Task<int> Add(Manager model);
        Task<Manager> Update(Manager model);
        Task<bool> Delete(int id);
        Task<Manager> GetById(int id);
        Task<Manager> GetByName(string name);
        Task<IEnumerable<Manager>> GetAll();
    }
    public class ManagerAPI: IManagerAPI
    {
        private readonly IRepositoryManager rm;

        public ManagerAPI(IRepositoryManager rm)
        {
            this.rm = rm;
        }

        public async Task<int> Add(Manager model)
        {
            return (await this.rm.Manager.Add(model)).Id;
        }

        public async Task<bool> Delete(int id)
        {
            return await this.rm.Manager.Delete(id);
        }

        public async Task<IEnumerable<Manager>> GetAll()
        {
            return await this.rm.Manager.GetAll().ToListAsync();
        }

        public async Task<Manager> GetById(int id)
        {
            return await this.rm.Manager.GetById(id);
        }

        public async Task<Manager> GetByName(string name)
        {
            return await this.rm.Manager.GetByFilter(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Manager> Update(Manager model)
        {
            return await this.rm.Manager.Update(model);
        }
    }
}

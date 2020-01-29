using FutbolAPI.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FutbolAPI.Business.Repositories
{
    public interface IRepository<TEntity> where TEntity:class
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(int id);
        Task<bool> Delete(TEntity entity);
        Task<TEntity> GetById(int id);
        IQueryable<TEntity> GetByFilter(Expression<Func<TEntity,bool>> where);
        IQueryable<TEntity> GetAll();
    }
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly FutbolAPIContext context;

        public Repository(FutbolAPIContext context)
        {
            this.context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            try
            {
                context.Set<TEntity>().Add(entity);
                await Save();
                return entity;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                TEntity existing = await context.Set<TEntity>().FindAsync(id);
                context.Set<TEntity>().Remove(existing);
                await Save();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return context.Set<TEntity>();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<TEntity> GetById(int id)
        {
            try
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                context.Set<TEntity>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                await Save();
                return entity;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task Save()
        {
            await this.context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return this.context.Set<TEntity>().Where(where);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Delete(TEntity entity)
        {
            try
            {
                context.Set<TEntity>().Remove(entity);
                await Save();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

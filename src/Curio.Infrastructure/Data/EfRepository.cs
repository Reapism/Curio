using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Curio.SharedKernel.Bases;
using Curio.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Curio.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        private readonly CurioClientDbContext dbContext;

        public EfRepository(CurioClientDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public T Add<T>(T entity) where T : Entity
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();

            return entity;
        }

        public void Update<T>(T entity) where T : Entity
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            dbContext.Set<T>().Remove(entity);
            dbContext.SaveChanges();
        }

        public T GetById<T>(Guid id) where T : Entity
        {
            return dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public Task<T> GetByIdAsync<T>(Guid id) where T : Entity
        {
            return dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public List<T> List<T>() where T : Entity
        {
            return dbContext.Set<T>().ToList();
        }

        public Task<List<T>> ListAsync<T>() where T : Entity
        {
            return dbContext.Set<T>().ToListAsync();
        }

        public List<T> List<T>(Expression<Func<T, bool>> predicate) where T : Entity
        {
            return dbContext.Set<T>().Where(predicate).ToList();
        }

        public Task<List<T>> ListAsync<T>(Expression<Func<T, bool>> predicate) where T : Entity
        {
            return dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public IEnumerable<T> Add<T>(IEnumerable<T> entities) where T : Entity
        {
            dbContext.Set<T>().AddRange(entities);
            dbContext.SaveChanges();

            return entities;
        }

        public async Task<T> AddAsync<T>(T entity) where T : Entity
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> AddAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();

            return entities;
        }

        public async Task UpdateAsync<T>(T entity) where T : Entity
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : Entity
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}

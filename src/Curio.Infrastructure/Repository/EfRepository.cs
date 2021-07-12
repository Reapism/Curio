using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Curio.Persistence.Client;
using Curio.SharedKernel.Bases;
using Curio.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Curio.Infrastructure.Repository
{
    public class EfRepository : IRepository
    {
        private readonly CurioClientDbContext dbContext;

        public EfRepository(CurioClientDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add<T>(T entity) where T : Entity
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
        }

        public void AddRange<T>(IEnumerable<T> entities) where T : Entity
        {
            dbContext.Set<T>().AddRange(entities);
            dbContext.SaveChanges();
        }

        public async Task AddAsync<T>(T entity) where T : Entity
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
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

    public class EfRepository<T> : IRepository<T>
        where T : Entity
    {
        private readonly CurioClientDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public EfRepository(CurioClientDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
            dbContext.SaveChanges();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
            dbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public T GetById(Guid id)
        {
            var entity = dbSet.SingleOrDefault(e => e.Id == id);
            return entity;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await dbSet.SingleOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public List<T> List()
        {
            var entities = dbSet.ToList();
            return entities;
        }

        public List<T> List(Expression<Func<T, bool>> predicate)
        {
            var entities = dbSet.Where(predicate).ToList();
            return entities;
        }

        public async Task<List<T>> ListAsync()
        {
            var entities = await dbSet.ToListAsync();
            return entities;
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await dbSet.Where(predicate).ToListAsync();
            return entities;
        }

        public void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}

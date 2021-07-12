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
    public class EfReadOnlyRepository : IReadOnlyRepository
    {
        private readonly CurioClientDbContext dbContext;

        public EfReadOnlyRepository(CurioClientDbContext dbContext)
        {
            this.dbContext = dbContext;
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
    }

    public class EfReadOnlyRepository<T> : IReadOnlyRepository<T>
        where T : Entity
    {
        private readonly CurioClientDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public EfReadOnlyRepository(CurioClientDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
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
    }
}

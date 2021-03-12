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

        public T GetById<T>(Guid id) where T : BaseEntity
        {
            return dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity
        {
            return dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            return dbContext.Set<T>().ToListAsync();
        }

        public Task<List<T>> ListAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            return dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}

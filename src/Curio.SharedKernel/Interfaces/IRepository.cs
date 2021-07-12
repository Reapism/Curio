using System.Collections.Generic;
using System.Threading.Tasks;
using Curio.SharedKernel.Bases;

namespace Curio.SharedKernel.Interfaces
{
    /// <summary>
    /// Contract for CRUDing at generic method level.
    /// <para>Using this as a dependency allows you to reuse
    /// this dependency and querying multiple T's.</para>
    /// </summary>
    public interface IRepository : IReadOnlyRepository
    {
        void Add<T>(T entity) where T : Entity;
        void AddRange<T>(IEnumerable<T> entities) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;

        Task AddAsync<T>(T entity) where T : Entity;
        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : Entity;
        Task UpdateAsync<T>(T entity) where T : Entity;
        Task DeleteAsync<T>(T entity) where T : Entity;
    }

    /// <summary>
    /// Contract for CRUDing at generic interface level.
    /// <para>Using this as a dependency allows you to limit
    /// consumers to querying a <typeparamref name="T"/>.</para>
    /// </summary>
    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : Entity
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
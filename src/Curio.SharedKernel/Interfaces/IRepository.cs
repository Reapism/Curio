using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Curio.SharedKernel.Bases;

namespace Curio.SharedKernel.Interfaces
{
    /// <summary>
    /// Contract for CRUDing at generic method level.
    /// <para>Using this as a dependency allows you to reuse
    /// this dependency and querying multiple T's.</para>
    /// </summary>
    public interface IRepository
    {
        T Get<T>(Expression<Func<T, bool>> predicate) where T : Entity;
        T GetById<T>(Guid id) where T : Entity;
        List<T> List<T>() where T : Entity;
        List<T> List<T>(Expression<Func<T, bool>> predicate) where T : Entity;
        T Add<T>(T entity) where T : Entity;
        IEnumerable<T> Add<T>(IEnumerable<T> entities) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;

        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : Entity;
        Task<T> GetByIdAsync<T>(Guid id) where T : Entity;
        Task<List<T>> ListAsync<T>() where T : Entity;
        Task<List<T>> ListAsync<T>(Expression<Func<T, bool>> predicate) where T : Entity;
        Task<T> AddAsync<T>(T entity) where T : Entity;
        Task<IEnumerable<T>> AddAsync<T>(IEnumerable<T> entities) where T : Entity;
        Task UpdateAsync<T>(T entity) where T : Entity;
        Task DeleteAsync<T>(T entity) where T : Entity;
    }

    /// <summary>
    /// Contract for CRUDing at generic interface level.
    /// <para>Using this as a dependency allows you to limit
    /// consumers to querying a <typeparamref name="T"/>.</para>
    /// </summary>
    public interface IRepository<T>
        where T : Entity
    {
        T Get(Expression<Func<T, bool>> predicate);
        T GetById(Guid id);
        List<T> List();
        List<T> List(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }

    public interface ISecuredRepository<T>
        where T : Entity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
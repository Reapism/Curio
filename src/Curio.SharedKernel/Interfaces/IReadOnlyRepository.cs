using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Curio.SharedKernel.Bases;

namespace Curio.SharedKernel.Interfaces
{
    public interface IReadOnlyRepository
    {
        T GetById<T>(Guid id) where T : Entity;
        List<T> List<T>() where T : Entity;
        List<T> List<T>(Expression<Func<T, bool>> predicate) where T : Entity;

        Task<T> GetByIdAsync<T>(Guid id) where T : Entity;
        Task<List<T>> ListAsync<T>() where T : Entity;
        Task<List<T>> ListAsync<T>(Expression<Func<T, bool>> predicate) where T : Entity;
    }


    public interface IReadOnlyRepository<T>
        where T : Entity
    {
        T GetById(Guid id);
        List<T> List();
        List<T> List(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);
    }
}

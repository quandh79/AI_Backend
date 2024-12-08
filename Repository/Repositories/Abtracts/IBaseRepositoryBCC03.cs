using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IBaseRepositoryBCC03<T> : IDisposable where T : class, new()
    {
        Task<T> Add(T entity);
        Task<IEnumerable<T>> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task Commit();
        Task<bool> Delete(object id);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetSingle(Expression<Func<T, bool>> predicate);
        Task<T> Update(T entity);

        T LastOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order);
        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order);
        T UpdateCreateBy(T obj, Object id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        T Update(T obj, Object id);
    }
}
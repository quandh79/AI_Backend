using Common.Params.Base;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IBaseRepositorySql<T> : IDisposable where T : class
    {
        Task<bool> CheckExists(object id);
        Task<T> Create(T obj);
        Task<bool> Delete(object id);
        Task<ListResult<T>> GetAll(PagingParam param);
        Task<T> GetById(object id);
        Task<T> Update(T obj, object id);
        Task<bool> UpdateIsActive(bool isActive, object id);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate);
        Task AddRange(List<T> entities);
        Task UpdateRange(List<T> entities);
        Task RemoveRange(List<T> entities);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
    }
}
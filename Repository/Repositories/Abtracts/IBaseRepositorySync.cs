//using Repository.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace Repository.Repositories
//{
//    public interface IBaseRepositorySync<T>: IDisposable where T : class, new()
//    {
//        T LastOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order);
//        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order);
//        T FirstOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order);
//        T Add(T entity);
//        //IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
//        //void Commit();
//        bool Delete(Object id);
//        void Update(T entity);
//        void Update(IEnumerable<T> entity);
//        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
//        //IEnumerable<T> GetAll();
//        T GetSingle(Expression<Func<T, bool>> predicate);
//        T Update(T obj, Object id);
//        T UpdateCreateBy(T obj, Object id);
//        List<T> Sort(List<T> ls, string field, string direction);

//        List<TT> Sort<TT>(List<TT> ls, string field, string direction);
//        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);

//        Task<T> AddAsync(T entity);
//        Task UpdateAsync(T entity);
       
//    }
//}
//    using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Repository.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Repository.Repositories
//{
//    public class BaseRepositorySync<T> : IBaseRepositorySync<T> where T : class, new()
//    {
//        public BaseRepositorySync()
//        {
            
//        }

//        public T LastOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                return _dbContextSql.Set<T>().Where(predicate).OrderByDescending(order).FirstOrDefault();
//            }
//        }
//        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                return await _dbContextSql.Set<T>().Where(predicate).OrderByDescending(order).FirstOrDefaultAsync();
//            }
//        }
//        public T FirstOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                return _dbContextSql.Set<T>().Where(predicate).OrderBy(order).FirstOrDefault();
//            }
//        }

//        public List<T> Sort(List<T> ls, string field, string direction)
//        {
//            if (string.IsNullOrEmpty(field))
//                return ls;

//            if (string.IsNullOrEmpty(field))
//                direction = "asc";

//            if (direction.ToLower() == "asc")
//            {
//                ls = ls.OrderBy(x => x.GetType().GetProperty(field).GetValue(x)).ToList();
//            }
//            else
//            {
//                ls = ls.OrderByDescending(x => x.GetType().GetProperty(field).GetValue(x)).ToList();
//            }

//            return ls;
//        }

//        public List<TT> Sort<TT>(List<TT> ls, string field, string direction)
//        {
//            if (string.IsNullOrEmpty(field))
//                return ls;

//            if (string.IsNullOrEmpty(field))
//                direction = "asc";

//            if (direction.ToLower() == "asc")
//            {
//                ls = ls.OrderBy(x => x.GetType().GetProperty(field).GetValue(x)).ToList();
//            }
//            else
//            {
//                ls = ls.OrderByDescending(x => x.GetType().GetProperty(field).GetValue(x)).ToList();
//            }

//            return ls;
//        }

//        public void Dispose()
//        {
//            // Dispose of unmanaged resources.
//            //_dbContextSql.Dispose();
//            // Suppress finalization.
//            GC.SuppressFinalize(this);
//        }




//        #region new 
//        public T GetSingle(Expression<Func<T, bool>> predicate)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                return _dbContextSql.Set<T>().FirstOrDefault(predicate);
//            }
//        }

//        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                return await _dbContextSql.Set<T>().FirstOrDefaultAsync(predicate);
//            }
//        }
//        //public virtual IEnumerable<T> GetAll()
//        //{
//        //    using (var _dbContextSql = new DbContextSql())
//        //    {
//        //        return  _dbContextSql.Set<T>().ToList();
//        //    }
//        //}

//        //public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
//        //{
//        //    using (var _dbContextSql = new DbContextSql())
//        //    {
//        //        IQueryable<T> query = _dbContextSql.Set<T>();
//        //        foreach (var includeProperty in includeProperties)
//        //        {
//        //            query = query.Include(includeProperty);
//        //        }
//        //        return query.AsEnumerable();
//        //    }
//        //}

//        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                return _dbContextSql.Set<T>().Where(predicate).ToList();
//            }
//        }

//        public virtual T Add(T entity)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                _dbContextSql.Set<T>().Add(entity);
//                _dbContextSql.SaveChanges();
//                return entity;
//            }
//        }
//        public virtual async Task<T> AddAsync(T entity)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                await _dbContextSql.Set<T>().AddAsync(entity);
//                await _dbContextSql.SaveChangesAsync();
//                return entity;
//            }
//        }
//        public virtual void Update(IEnumerable<T> entities)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                foreach (var item in entities)
//                {
//                    EntityEntry dbEntityEntry = _dbContextSql.Entry<T>(item);
//                    dbEntityEntry.State = EntityState.Modified;
//                }
//                _dbContextSql.SaveChanges();
//            }
//        }

//        public virtual void Update(T entity)
//        {
//            using(var _dbContextSql = new DbContextSql())
//            {
//                EntityEntry dbEntityEntry = _dbContextSql.Entry<T>(entity);
//                dbEntityEntry.State = EntityState.Modified;
//                _dbContextSql.SaveChanges();
//            }
//        }
//        public virtual async Task UpdateAsync(T entity)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                EntityEntry dbEntityEntry = _dbContextSql.Entry<T>(entity);
//                dbEntityEntry.State = EntityState.Modified;
//                await _dbContextSql.SaveChangesAsync();
//            }
//        }
//        public virtual T Update(T obj, Object id)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                var entity =  _dbContextSql.Set<T>().Find(id);
//                if (entity.GetType().GetProperty("tenant_id") != null)
//                {
//                    obj.GetType().GetProperty("tenant_id").SetValue(obj, entity.GetType().GetProperty("tenant_id").GetValue(entity));
//                }
//                obj.GetType().GetProperty("create_by").SetValue(obj, entity.GetType().GetProperty("create_by").GetValue(entity));
//                obj.GetType().GetProperty("create_time").SetValue(obj, entity.GetType().GetProperty("create_time").GetValue(entity));
//                _dbContextSql.Entry(entity).CurrentValues.SetValues(obj);
//                EntityEntry dbEntityEntry = _dbContextSql.Entry<T>(entity);
//                dbEntityEntry.State = EntityState.Modified;
//                 _dbContextSql.SaveChanges();
//                return entity;
//            }
//        }


//        public virtual T UpdateCreateBy(T obj, Object id)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                var entity =  _dbContextSql.Set<T>().Find(id);
//                if (entity.GetType().GetProperty("tenant_id") != null)
//                {
//                    obj.GetType().GetProperty("tenant_id").SetValue(obj, entity.GetType().GetProperty("tenant_id").GetValue(entity));
//                }
//                obj.GetType().GetProperty("create_time").SetValue(obj, entity.GetType().GetProperty("create_time").GetValue(entity));
//                _dbContextSql.Entry(entity).CurrentValues.SetValues(obj);
//                EntityEntry dbEntityEntry = _dbContextSql.Entry<T>(entity);
//                dbEntityEntry.State = EntityState.Modified;
//                 _dbContextSql.SaveChanges();
//                return entity;
//            }
//        }

//        public virtual bool Delete(Object id)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                var entity = _dbContextSql.Set<T>().Find(id);
//                EntityEntry dbEntityEntry = _dbContextSql.Entry<T>(entity);
//                dbEntityEntry.State = EntityState.Deleted;
//                 _dbContextSql.SaveChanges();
//                return true;
//            }
//        }

//        //public virtual void Commit()
//        //{
//        //    //await _dbContextSql.SaveChangesAsync();
//        //    ////Dispose();
//        //    using (var _dbContextSql = new DbContextSql())
//        //    {
//        //        _dbContextSql.SaveChanges();
//        //    }
//        //}

//        #endregion
//    }
//}

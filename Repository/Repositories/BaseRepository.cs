//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Org.BouncyCastle.Asn1.X509;
//using Repository.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace Repository.Repositories
//{
//    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
//    {

//        public BaseRepository()
//        {
//        }

//        public async Task<T> GetSingle(Expression<Func<T, bool>> predicate)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                return await _dbContextSql.Set<T>().FirstOrDefaultAsync(predicate);
//            }
            
//        }

//        public virtual async Task<IEnumerable<T>> GetAll()
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                return await _dbContextSql.Set<T>().ToListAsync();
//            }
                
//        }

//        public virtual async Task<IEnumerable<T>> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                IQueryable<T> query = _dbContextSql.Set<T>();
//                foreach (var includeProperty in includeProperties)
//                {
//                    query = query.Include(includeProperty);
//                }
//                return await Task.FromResult(query.AsEnumerable());
//            }    
                
//        }

//        public virtual async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                return await _dbContextSql.Set<T>().Where(predicate).ToListAsync();
//            }
//        }

//        public virtual async Task<T> Add(T entity)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                _dbContextSql.Set<T>().Add(entity);
//                await _dbContextSql.SaveChangesAsync();
//                return entity;
//            }
//        }

//        public virtual async Task<T> Update(T entity)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                EntityEntry dbEntityEntry = _dbContextSql.Entry<T>(entity);
//                dbEntityEntry.State = EntityState.Modified;
//                await _dbContextSql.SaveChangesAsync();
//                return entity;
//            }
//        }


//        public virtual async Task<bool> Delete(Object id)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                var entity = await _dbContextSql.Set<T>().FindAsync(id);
//                EntityEntry dbEntityEntry = _dbContextSql.Entry<T>(entity);
//                dbEntityEntry.State = EntityState.Deleted;
//                await _dbContextSql.SaveChangesAsync();
//                return true;
//            }
//        }

//        public virtual async Task Commit()
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                await _dbContextSql.SaveChangesAsync();
//            }
//        }

//        public void Dispose()
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                // Dispose of unmanaged resources.
//                _dbContextSql.Dispose();
//                // Suppress finalization.
//                GC.SuppressFinalize(this);
//            }
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
//        public virtual T UpdateCreateBy(T obj, Object id)
//        {
//            using (var _dbContextSql = new DbContextSql())
//            {
//                var entity = _dbContextSql.Set<T>().Find(id);
//                if (entity.GetType().GetProperty("tenant_id") != null)
//                {
//                    obj.GetType().GetProperty("tenant_id").SetValue(obj, entity.GetType().GetProperty("tenant_id").GetValue(entity));
//                }
//                obj.GetType().GetProperty("create_time").SetValue(obj, entity.GetType().GetProperty("create_time").GetValue(entity));
//                _dbContextSql.Entry(entity).CurrentValues.SetValues(obj);
//                EntityEntry dbEntityEntry = _dbContextSql.Entry<T>(entity);
//                dbEntityEntry.State = EntityState.Modified;
//                _dbContextSql.SaveChanges();
//                return entity;
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
//                var entity = _dbContextSql.Set<T>().Find(id);
//                if (entity.GetType().GetProperty("tenant_id") != null)
//                {
//                    obj.GetType().GetProperty("tenant_id").SetValue(obj, entity.GetType().GetProperty("tenant_id").GetValue(entity));
//                }
//                obj.GetType().GetProperty("create_by").SetValue(obj, entity.GetType().GetProperty("create_by").GetValue(entity));
//                obj.GetType().GetProperty("create_time").SetValue(obj, entity.GetType().GetProperty("create_time").GetValue(entity));
//                _dbContextSql.Entry(entity).CurrentValues.SetValues(obj);
//                EntityEntry dbEntityEntry = _dbContextSql.Entry<T>(entity);
//                dbEntityEntry.State = EntityState.Modified;
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
//    }
//}

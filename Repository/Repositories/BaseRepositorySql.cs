using Common.Params.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.EF;
using Repository.Model;
using Repository.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepositorySql<T> : IBaseRepositorySql<T> where T : class
    {
        protected readonly BCC01_DbContextSql _db;
        public BaseRepositorySql()
        {
            _db = new BCC01_DbContextSql();
        }
        #region implement
        public virtual async Task<T> Create(T obj)
        {
            _db.Set<T>().Add(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public virtual async Task<T> GetById(Object id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public virtual async Task<ListResult<T>> GetAll(PagingParam param)
        {
            Query<T> query = new Query<T>(param, _db);

            List<T> datas = await query.ToListAsync();
            int total = await query.CountAsync();

            return new ListResult<T>(datas, total);
        }

        public async virtual Task<T> Update(T obj, Object id)
        {
            var entity = await _db.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return null;
            }
            if (obj.GetType().GetProperty("password") != null && obj.GetType().GetProperty("password").GetValue(obj) == null)
            {
                obj.GetType().GetProperty("password").SetValue(obj, entity.GetType().GetProperty("password").GetValue(entity));
            }
            if (entity.GetType().GetProperty("tenant_id") != null)
            {
                obj.GetType().GetProperty("tenant_id").SetValue(obj, entity.GetType().GetProperty("tenant_id").GetValue(entity));
            }
            if (entity.GetType().GetProperty("role_parent_id") != null)
            {
                obj.GetType().GetProperty("role_parent_id").SetValue(obj, entity.GetType().GetProperty("role_parent_id").GetValue(entity));
            }
            obj.GetType().GetProperty("create_by").SetValue(obj, entity.GetType().GetProperty("create_by").GetValue(entity));
            obj.GetType().GetProperty("create_time").SetValue(obj, entity.GetType().GetProperty("create_time").GetValue(entity));
            _db.Entry(entity).CurrentValues.SetValues(obj);
            await _db.SaveChangesAsync();
            return entity;

        }
        public async virtual Task<bool> Delete(Object id)
        {
            var itemDelete = await _db.Set<T>().FindAsync(id);
            if (itemDelete == null) return false;
            _db.Set<T>().Remove(itemDelete);
            await _db.SaveChangesAsync();
            return true;
        }
        public async virtual Task<bool> UpdateIsActive(bool isActive, Object id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
            _db.Entry(entity).Property("is_active").CurrentValue = isActive;
            await _db.SaveChangesAsync();
            return true;
        }
        public async virtual Task<bool> CheckExists(Object id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            return true;
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
        public virtual async Task AddRange(List<T> entities)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.Set<T>().AddRangeAsync(entities);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }
        public virtual async Task UpdateRange(List<T> entities)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Set<T>().UpdateRange(entities);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }
        public virtual async Task RemoveRange(List<T> entities)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Set<T>().RemoveRange(entities);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }
        public virtual async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        #endregion

    }
}

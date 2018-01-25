using eCafe.Core.Entities;
using eCafe.Core.Repository;
using eCafe.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
            where T : class, IEntityBase, new()
    {

        private ECafeContext _context;

        #region Properties
        public EntityBaseRepository(ECafeContext context)
        {
            _context = context;
        }
        #endregion


        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsNoTracking().AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public T GetSingle(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T  GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public async Task<T> GetSingleAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsNoTracking().ToList();
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            _context.Set<T>().Add(entity);
        }

        public virtual void Edit(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }

        public virtual IQueryable<T> GetAllAsync(Int32 pageSize, Int32 pageNumber, String name)
        {
            var query = _context
                        .Set<T>()
                        .AsNoTracking()
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);

            if (!String.IsNullOrEmpty(name))
            {
                //query = query
                //        .Where(item => item.name.ToLower()
                //        .Contains(name.ToLower()));
            }

            return query;
        }       

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context
                    .Set<T>()
                    .AsNoTracking()
                    .Where(predicate);
        }

        public virtual Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            return _context
                    .Set<T>()
                    .Where(filter)
                    .CountAsync();
        }

        public virtual Task<bool> IsExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            return _context
                     .Set<T>()
                     .Where(filter)
                     .AnyAsync();
        }

        public virtual Task<T> GetAsync(int id)
        {
            return _context
                    .Set<T>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(item => item.Id == id);
        }

        public virtual Task<T> GetAsync(T entity)
        {
            return _context
                    .Set<T>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(item => item.Id == entity.Id);
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var result = await GetAsync(entity);

            if (result != null)
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                result = entity;
            }
            return result;
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            var result = await GetAsync(entity);

            if (result != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }
    }
}

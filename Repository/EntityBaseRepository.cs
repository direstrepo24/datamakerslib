using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using datamakerslib.DataContext;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks; 

namespace datamakerslib.Repository
{
    public class EntityBaseRepository<T, Tkey, TContext> :RepositoryBase<TContext>, IEntityBaseRepository<T,Tkey>
           where TContext : DbContext where T : class, IEntityBase<Tkey>, new()
    {
        
             private TContext  _context;

         #region Properties
       protected EntityBaseRepository(TContext context) : base(context)
		{
            _context=context;
         }
        #endregion
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        
        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
             
            return query.AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            
            return await query.AsQueryable().ToListAsync();//ToListAsync();
        }
         public virtual async Task<IEnumerable<T>> AllIncludingAsyncWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            
            return await query.Where(predicate).AsQueryable().ToListAsync();//ToListAsync();
        }
        public virtual async Task<T> AllIncludingAsyncWhereSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            
            return await query.Where(predicate).AsQueryable().FirstOrDefaultAsync();//ToListAsync();
        }
        //https://stackoverflow.com/questions/35779723/how-to-implement-generic-getbyid-where-id-can-be-of-various-types
        public T GetSingle(Tkey id)
        {
            return _context.Set<T>().Find(id);//FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();//FirstOrDefault();
        }
         public async Task<List<T>> GetSAsyncOrder(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty).OrderByDescending(includeProperty);
            }

            return await query.Where(predicate).ToListAsync();//FirstOrDefault();
        }

        public async Task<T> GetSingleAsync(Tkey id)
        {
            return await _context.Set<T>().FindAsync(id);//FirstOrDefaultAsync(id);
        }
        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }
        
        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
           
          var lista=  await _context.Set<T>().Where(predicate).ToListAsync();
             return lista;
        }

        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            _context.Set<T>().Add(entity);
        }
        public virtual async Task AddAsync(T entity){

                 EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                await _context.Set<T>().AddAsync(entity);
        }
        public virtual void Edit(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public async Task<T> EditAsync(T entity) 
        { 
             var edited = _context.Set<T>().Update(entity);
            await Commit();//_context.SaveChangesAsync();
            return edited.Entity;
           // await _context.SaveChangesAsync(); 
        } 
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual async Task Commit()
        {
           await _context.SaveChangesAsync();
        }
         public virtual void Close()
        {
            _context.Dispose();//Database.CloseConnection();
        }

       
    }
}
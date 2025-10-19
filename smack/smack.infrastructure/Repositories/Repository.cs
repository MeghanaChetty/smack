using System;
using smack.core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smack.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace smack.infrastructure.Repositories
{
    public class Repository<T>  : IRepository<T> where T : class
    {
        protected readonly SmackDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(SmackDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        // --- WRITE Operations ---

        /// <summary>
        /// Adds a new entity to the context for tracking.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task AddAsync(T entity)
        {
            // AddAsync stages the entity for insertion when SaveChanges is called.
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Marks an existing entity as modified in the context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void  UpdateAsync(T entity)
        {
        
            _dbSet.Update(entity);
        }


        public void DeleteAsync(T entity)
        {
            
            _dbSet.Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            // We use FindAsync(id) which returns the object (T) or null, 
            // and check if the result is not null.
            return await _dbSet.FindAsync(id) != null;
        }

    }
}

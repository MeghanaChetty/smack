using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace smack.infrastructure.Interfaces
{
   public interface IRepository<T>
    {
        //GetByIdAsync(int id) - returns single entity or null
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(int id);
        Task<int> CountAsync();

        //AddAsync(T entity) - add new entity
        //UpdateAsync(T entity) - update entity
        //DeleteAsync(T entity) - delete entity
        //ExistsAsync(int id) - check if exists
        //CountAsync() - count entities



    }
    
    
}

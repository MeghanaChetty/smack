using smack.core.Interfaces;
using smack.infrastructure.Data;
using smack.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace smack.infrastructure.Repositories
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(SmackDbContext context) : base(context)
        {
            
        }

        public async Task<Restaurant?> GetRestaurantWithMenuItemsAsync(int restaurantId)
        {
            return await _dbSet
                .Include(r => r.Menuitems)
                .ThenInclude(m=>m.CategoryNavigation)
                .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);
        }

        public async Task<Restaurant?> GetRestaurantWithTablesAsync(int restaurantId)
        {
            return await _dbSet
                .Include(r => r.Restauranttables)
                .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);
        }
        public async Task<IEnumerable<Restaurant>> GetActiveRestaurantsAsync()
        {
            return await _dbSet
                .Where(r => r.IsActive == true)
                .ToListAsync();
        }



    }
}

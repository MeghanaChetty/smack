using smack.core.Interfaces;
using smack.infrastructure.Data;
using smack.core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.infrastructure.Repositories
{
    public class MenuItemRepository : Repository<Menuitem>, IMenuItemRepository
    {
        public MenuItemRepository(SmackDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Menuitem>> GetMenuItemsByRestaurantAsync(int restaurantId)
        {
            return await _dbSet.Where(mi => mi.RestaurantId == restaurantId).ToListAsync();
        }
        public async Task<IEnumerable<Menuitem>> GetAvailableMenuItemsAsync(int restaurantId)
        {
            return await _dbSet
                .Where(mi => mi.RestaurantId == restaurantId && mi.Isavailable == true)
                .ToListAsync();
        }

    }
}

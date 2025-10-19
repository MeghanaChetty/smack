using smack.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.core.Interfaces
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<Restaurant?> GetRestaurantWithMenuItemsAsync(int restaurantId);
        Task<Restaurant?> GetRestaurantWithTablesAsync(int restaurantId);
        Task<IEnumerable<Restaurant>> GetActiveRestaurantsAsync();
    }
        
}

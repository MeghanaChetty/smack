using smack.core.Entities;
using smack.infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.core.Interfaces
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<int> GetRestaurantWithMenuItemsAsync(int restaurantId);
        Task<int> GetRestaurantWithTablesAsync(int restaurantId);
        Task<IEnumerable<Restaurant>> GetActiveRestaurantsAsync();
    }
        
}

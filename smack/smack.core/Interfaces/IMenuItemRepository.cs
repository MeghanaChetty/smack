using smack.core.Entities;
using smack.infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.core.Interfaces
{
    public interface IMenuItemRepository : IRepository<Menuitem>
    {
        Task<IEnumerable<Menuitem>> GetMenuItemsByRestaurantAsync(int restaurantId);
        Task<IEnumerable<Menuitem>> GetAvailableMenuItemsAsync(int restaurantId);
    }
}

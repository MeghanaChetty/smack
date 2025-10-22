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
    public class OrderRepository: Repository<Order>, IOrderRepository
    {
        public OrderRepository(SmackDbContext context) :  base(context) {
        }

       public async Task<IEnumerable<Order>> GetActiveOrdersForRestaurantAsync(int restaurantId)
        {
            return await _dbSet.Where(o => o.RestaurantId == restaurantId && (o.Status == 1 || o.Status == 2) && o.Islive == true)
                               .OrderByDescending(o => o.Orderdate)
                               .ToListAsync();
        }
        public async Task<Order> GetOrderWithItemsAsync(int orderId)
        {
            return await _dbSet.Include(o => o.Orderitems)
                .OrderByDescending(o => o.Orderdate)
                 .FirstOrDefaultAsync(o => o.OrderId == orderId && o.Islive == true);
        }
        public async    Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId) { 
            return await _dbSet.Where(o => o.UserId == userId && o.Islive == true)
                .OrderByDescending(o => o.Orderdate)
                .ToListAsync();

        }
    }
}

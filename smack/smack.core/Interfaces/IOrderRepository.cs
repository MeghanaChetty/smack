using smack.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>  
    {
        Task<IEnumerable<Order>> GetActiveOrdersForRestaurantAsync(int restaurantId);
        Task<Order> GetOrderWithItemsAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId);
    }
}


//4.IOrderRepository.cs

//Inherits from IRepository<Order>
//Additional methods:

//GetActiveOrdersForRestaurantAsync(int restaurantId) - pending / preparing orders
//GetOrderWithItemsAsync(int orderId) - includes order items
//GetOrdersByUserAsync(int userId) - user's order history
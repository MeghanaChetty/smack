using smack.core.Entities;
using smack.infrastructure.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace smack.core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRestaurantRepository Restaurants { get; }
        IUserRepository Users { get; }
        IMenuItemRepository MenuItems { get; }
        IOrderRepository Orders { get; }
        IRepository<Role> Roles { get; }
       
        IRepository<Category> Categories { get; }
        IRepository<Restauranttable> RestaurantTables { get; }
        IRepository<Orderitem> OrderItems { get; }
        IRepository<Orderstatus> OrderStatuses { get; }
        IRepository<UserRestaurant> UserRestaurants { get; }


       public  Task<int> SaveChangesAsync();
        //- save all changes
        public Task BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using smack.core.Entities;
using smack.core.Interfaces;
using smack.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private SmackDbContext _context;
        private IDbContextTransaction? _transaction;
        private IRestaurantRepository? _restaurants;
        private IUserRepository? _users;
        private IMenuItemRepository? _menuItems;
        private IOrderRepository? _orders;
        private IRepository<Role>? _roles;
        private IRepository<Category>? _categories;
        private IRepository<Restauranttable>? _restaurantTables;
        private IRepository<Orderitem>? _orderItems;
        private IRepository<Orderstatus>? _orderStatuses;
        private IRepository<UserRestaurant>? _userRestaurants;
        public UnitOfWork(SmackDbContext context)   
        {
            _context = context;
        }

        public IRestaurantRepository Restaurant
        {
            get
            {
                if (_restaurants == null)
                {
                    _restaurants = new RestaurantRepository(_context);
                }

                return _restaurants;
            }

        }
        public IUserRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_context);
                }
                return _users;
            }
        }

        public IMenuItemRepository MenuItems
        {
            get
            {
                if (_menuItems == null)
                {
                    _menuItems = new MenuItemRepository(_context);
                }
                return _menuItems;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (_orders == null)
                {
                    _orders = new OrderRepository(_context);
                }
                return _orders;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new Repository<Role>(_context);
                }
                return _roles;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new Repository<Category>(_context);
                }
                return _categories;
            }
        }

        public IRepository<Restauranttable> RestaurantTables
        {
            get
            {
                if (_restaurantTables == null)
                {
                    _restaurantTables = new Repository<Restauranttable>(_context);
                }
                return _restaurantTables;
            }
        }

        public IRepository<Orderitem> OrderItems
        {
            get
            {
                if (_orderItems == null)
                {
                    _orderItems = new Repository<Orderitem>(_context);
                }
                return _orderItems;
            }
        }

        public IRepository<Orderstatus> OrderStatuses
        {
            get
            {
                if (_orderStatuses == null)
                {
                    _orderStatuses = new Repository<Orderstatus>(_context);
                }
                return _orderStatuses;
            }
        }

        public IRepository<UserRestaurant> UserRestaurants
        {
            get
            {
                if (_userRestaurants == null)
                {
                    _userRestaurants = new Repository<UserRestaurant>(_context);
                }
                return _userRestaurants;
            }
        }

        // Save changes
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Transaction methods
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        // Dispose
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }



    }
}

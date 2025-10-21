using smack.core.Interfaces;
using smack.infrastructure.Data;
using smack.core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace smack.infrastructure.Repositories
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(SmackDbContext context) : base(context)
        {

        }
        //UserRepository.cs

        //Inherits Repository<User> and implements IUserRepository
        //Implement email/googleId lookups
        //For GetUserRestaurantsAsync, query UserRestaurants junction table
        public async Task<User?> GetByEmailAsync(string email) { 
             return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<User?> GetByGoogleIdAsync(string googleId) { 
            return await _dbSet.FirstOrDefaultAsync(u => u.GoogleId == googleId);
        }


        public async Task<IEnumerable<Restaurant>> GetUserRestaurantsAsync(int userId) {
            return await _context.UserRestaurants
            .Where(ur => ur.UserId == userId)
            .Join(_context.Restaurants,
               ur => ur.RestaurantId,
               r => r.RestaurantId,
               (ur, r) => r)
            .ToListAsync();
        }
    }
}

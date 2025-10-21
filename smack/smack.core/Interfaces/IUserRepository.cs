using smack.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByGoogleIdAsync(string googleId);

        
        Task<IEnumerable<Restaurant>> GetUserRestaurantsAsync(int userId);
    }
}

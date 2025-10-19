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
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Retrieves a user entity based on their Google external authentication ID.
        /// </summary>
        /// <param name="googleId">The Google ID string.</param>
        /// <returns>The user entity, or null.</returns>
        Task<User> GetByGoogleIdAsync(string googleId);

        /// <summary>
        /// Retrieves a collection of restaurants that the specified user has access to manage.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of accessible restaurant entities.</returns>
        Task<IEnumerable<Restaurant>> GetUserRestaurantsAsync(int userId);
    }
}

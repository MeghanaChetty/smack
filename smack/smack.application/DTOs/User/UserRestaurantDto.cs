using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smack.application.DTOs.Restaurant;

namespace smack.application.DTOs.User
{
    public class UserRestaurantDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; }

        public string Email { get; set; }

        public List<RestaurantDto> Restaurants { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.application.DTOs.Restaurant
{
    public class CreateRestaurantDto
    {
     
        public string RestaurantName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}

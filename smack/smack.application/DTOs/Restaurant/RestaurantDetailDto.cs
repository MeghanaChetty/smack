using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.application.DTOs.Restaurant
{
    public class RestaurantDetailDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }

        public List<MenuItemDto> MenuItems { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace smack.core.Entities;

public partial class Restaurant
{
    public int RestaurantId { get; set; }

    public string RestaurantName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Restauranttable> Restauranttables { get; set; } = new List<Restauranttable>();

    public virtual ICollection<UserRestaurant> UserRestaurants { get; set; } = new List<UserRestaurant>();
}

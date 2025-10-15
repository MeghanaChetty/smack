using System;
using System.Collections.Generic;

namespace smack.core.Entities;
public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string? GoogleId { get; set; }

    public string Username { get; set; } = null!;

    public int Usertype { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<UserRestaurant> UserRestaurants { get; set; } = new List<UserRestaurant>();

    public virtual Role UsertypeNavigation { get; set; } = null!;
}

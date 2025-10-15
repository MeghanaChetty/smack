using System;
using System.Collections.Generic;

namespace smack.core.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string Rolename { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserRestaurant> UserRestaurants { get; set; } = new List<UserRestaurant>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

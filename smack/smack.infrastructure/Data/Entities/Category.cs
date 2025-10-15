using System;
using System.Collections.Generic;

namespace smack.core.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Categoryname { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();
}

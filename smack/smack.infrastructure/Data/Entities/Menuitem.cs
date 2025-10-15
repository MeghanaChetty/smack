using System;
using System.Collections.Generic;

namespace smack.core.Entities;

public partial class Menuitem
{
    public int Id { get; set; }

    public int RestaurantId { get; set; }

    public string Itemname { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Category { get; set; }

    public bool? Isavailable { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Createdby { get; set; }

    public virtual Category CategoryNavigation { get; set; } = null!;

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}

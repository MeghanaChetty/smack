using System;
using System.Collections.Generic;

namespace smack.core.Entities;

public partial class Restauranttable
{
    public int TableId { get; set; }

    public int RestaurantId { get; set; }

    public int TableNumber { get; set; }

    public string? Qrcode { get; set; }

    public bool? Isoccupied { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}

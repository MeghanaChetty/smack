using System;
using System.Collections.Generic;

namespace smack.core.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int RestaurantId { get; set; }

    public int TableId { get; set; }

    public int UserId { get; set; }

    public DateTime? Orderdate { get; set; }

    public decimal Totalamount { get; set; }

    public int Status { get; set; }

    public bool? Islive { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual Orderstatus StatusNavigation { get; set; } = null!;

    public virtual Restauranttable Table { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

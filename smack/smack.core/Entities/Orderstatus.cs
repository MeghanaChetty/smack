using System;
using System.Collections.Generic;

namespace smack.core.Entities;

public partial class Orderstatus
{
    public int Id { get; set; }

    public string Statusname { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

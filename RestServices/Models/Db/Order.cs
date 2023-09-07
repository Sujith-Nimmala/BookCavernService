using System;
using System.Collections.Generic;

namespace RestServices.Models.Db;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? PlacedOn { get; set; }

    public virtual AppUser? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

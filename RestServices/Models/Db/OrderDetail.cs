using System;
using System.Collections.Generic;

namespace RestServices.Models.Db;

public partial class OrderDetail
{
    public int? OrderId { get; set; }

    public int? BookId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Cost { get; set; }

    public int OrderNo { get; set; }

    public virtual BookDetail? Book { get; set; }

    public virtual Order? Order { get; set; }
}

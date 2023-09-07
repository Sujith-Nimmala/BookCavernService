using System;
using System.Collections.Generic;

namespace RestServices.Models.Db;

public partial class BookDetail
{
    public int BookId { get; set; }

    public string? BookName { get; set; }

    public string? AuthorName { get; set; }

    public decimal? BookPrice { get; set; }

    public int? Stock { get; set; }

    public string? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

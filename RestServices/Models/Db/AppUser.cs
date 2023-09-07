using System;
using System.Collections.Generic;

namespace RestServices.Models.Db;

public partial class AppUser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserAddress { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPass { get; set; }

    public string? UserContactNo { get; set; }

    public string? UserRole { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

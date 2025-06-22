using System;
using System.Collections.Generic;

namespace Dal_Repository.Models;

public partial class Company
{
    public short Id { get; set; }

    public string? NameC { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

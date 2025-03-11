using System;
using System.Collections.Generic;

namespace Dal_Repository.models;

public partial class Client
{
    public string Id { get; set; } = null!;

    public string? NameC { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? BearthDate { get; set; }

    public virtual ICollection<Buy> Buys { get; set; } = new List<Buy>();
}

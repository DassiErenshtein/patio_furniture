using System;
using System.Collections.Generic;

namespace Dal_Repository.Models;

public partial class Product
{
    public short Id { get; set; }

    public string? NameP { get; set; }

    public short? CodeCat { get; set; }

    public short? CodeCom { get; set; }

    public string? Descrip { get; set; }

    public short? Price { get; set; }

    public string? Pic { get; set; }

    public short? Amount { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual Category? CodeCatNavigation { get; set; }

    public virtual Company? CodeComNavigation { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
}

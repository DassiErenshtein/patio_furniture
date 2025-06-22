using System;
using System.Collections.Generic;

namespace Dal_Repository.Models;

public partial class Buy
{
    public short Id { get; set; }

    public string? CodeClient { get; set; }

    public DateTime? Date { get; set; }

    public float? SumPrice { get; set; }

    public string? Note { get; set; }

    public sbyte? StatusBuy { get; set; }

    public virtual Client? CodeClientNavigation { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
}

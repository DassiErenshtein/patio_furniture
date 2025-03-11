using System;
using System.Collections.Generic;

namespace Dal_Repository.models;

public partial class Buy
{
    public short Id { get; set; }

    public string? CodeClient { get; set; }

    public DateTime? Date { get; set; }

    public double? SumPrice { get; set; }

    public string? Note { get; set; }

    public bool? StatusBuy { get; set; }

    public virtual Client? CodeClientNavigation { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
}

using System;
using System.Collections.Generic;

namespace DTO_Command;

public partial class Buy
{
    public short Id { get; set; }

    public string? CodeClient { get; set; }

    public DateTime? Date { get; set; }

    public double? SumPrice { get; set; }

    public string? Note { get; set; }

    public List<Product>? products { get; set; }
    public List<Product>? finished { get; set; }
}

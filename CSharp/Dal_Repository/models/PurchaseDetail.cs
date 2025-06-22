using System;
using System.Collections.Generic;

namespace Dal_Repository.Models;

public partial class PurchaseDetail
{
    public short Id { get; set; }

    public short? CodeBuy { get; set; }

    public short? CodeProd { get; set; }

    public short? Amount { get; set; }

    public virtual Buy? CodeBuyNavigation { get; set; }

    public virtual Product? CodeProdNavigation { get; set; }
}

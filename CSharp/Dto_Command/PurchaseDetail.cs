using System;
using System.Collections.Generic;

namespace DTO_Command;

public partial class PurchaseDetail
{
    public short Id { get; set; }

    public short? CodeBuy { get; set; }

    public short? CodeProd { get; set; }

    public short? Amount { get; set; }

    public int PriceBuy { get; set; }

}

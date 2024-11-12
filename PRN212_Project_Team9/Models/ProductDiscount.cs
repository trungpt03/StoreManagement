using System;
using System.Collections.Generic;

namespace PRN212_Project_Team9.Models;

public partial class ProductDiscount
{
    public int ProductDiscountId { get; set; }

    public int ProductId { get; set; }

    public int DiscountId { get; set; }

    public virtual Discount Discount { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

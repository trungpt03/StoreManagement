using System;
using System.Collections.Generic;

namespace PRN212_Project_Team9.Models;

public partial class SupplierProduct
{
    public int SupplierProductId { get; set; }

    public int SupplierId { get; set; }

    public int ProductId { get; set; }

    public decimal SupplierPrice { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}

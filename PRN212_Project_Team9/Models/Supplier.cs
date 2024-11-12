using System;
using System.Collections.Generic;

namespace PRN212_Project_Team9.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>();
}

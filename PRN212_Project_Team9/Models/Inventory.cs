using System;
using System.Collections.Generic;

namespace PRN212_Project_Team9.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual Product Product { get; set; } = null!;
}

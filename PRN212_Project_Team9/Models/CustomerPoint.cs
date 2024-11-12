using System;
using System.Collections.Generic;

namespace PRN212_Project_Team9.Models;

public partial class CustomerPoint
{
    public int CustomerPointId { get; set; }

    public int CustomerId { get; set; }

    public int ProgramId { get; set; }

    public int? Points { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual LoyaltyProgram Program { get; set; } = null!;
}

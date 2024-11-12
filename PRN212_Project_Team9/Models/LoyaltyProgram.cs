using System;
using System.Collections.Generic;

namespace PRN212_Project_Team9.Models;

public partial class LoyaltyProgram
{
    public int ProgramId { get; set; }

    public string ProgramName { get; set; } = null!;

    public decimal? PointMultiplier { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<CustomerPoint> CustomerPoints { get; set; } = new List<CustomerPoint>();
}

using System;
using System.Collections.Generic;

namespace PRN212_Project_Team9.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int OrderId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public decimal AmountPaid { get; set; }

    public virtual Order Order { get; set; } = null!;
}

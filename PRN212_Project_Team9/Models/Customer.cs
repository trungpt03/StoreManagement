using System;
using System.Collections.Generic;

namespace PRN212_Project_Team9.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<CustomerPoint> CustomerPoints { get; set; } = new List<CustomerPoint>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

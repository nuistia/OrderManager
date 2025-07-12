using System;
using System.Collections.Generic;

namespace Hometask.OrderManager.Data.Models;

public partial class Analysis
{
    public Guid AnId { get; set; }

    public string? AnName { get; set; }

    public decimal? AnCost { get; set; }

    public decimal? AnPrice { get; set; }

    public Guid? AnGroup { get; set; }

    public virtual Group? AnGroupNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

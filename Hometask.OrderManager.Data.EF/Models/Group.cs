using System;
using System.Collections.Generic;

namespace Hometask.OrderManager.Data.Models;

public partial class Group
{
    public Guid GrId { get; set; }

    public string? GrName { get; set; }

    public decimal? GrTemp { get; set; }

    public virtual ICollection<Analysis> Analyses { get; set; } = new List<Analysis>();
}

namespace Hometask.OrderManager.Data.EF.Models;

public partial class Group : IEntity
{
    public Guid GrId { get; set; }

    public string? GrName { get; set; }

    public decimal? GrTemp { get; set; }

    public virtual ICollection<Analysis> Analyses { get; set; } = new List<Analysis>();
}

namespace Hometask.OrderManager.Data.Models;

public partial class Order : IEntity
{
    public Guid OrdId { get; set; }

    public DateTime? OrdDatetime { get; set; }

    public Guid? OrdAn { get; set; }

    public virtual Analysis? OrdAnNavigation { get; set; }
}

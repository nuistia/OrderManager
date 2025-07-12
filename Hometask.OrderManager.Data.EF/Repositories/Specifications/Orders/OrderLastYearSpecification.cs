using Hometask.OrderManager.Data;
using Hometask.OrderManager.Data.Models;

namespace Hometask.OrderManager.Data.EF.Repositories.Specifications.Orders;

public class OrderLastYearSpecification : Specification<Order>
{
    public OrderLastYearSpecification()
    {
        var yearNow = DateTime.Now.Year;
        Criteria = order => order.OrdDatetime.Value.Year >= yearNow - 1;
        AddInclude(o => o.OrdAnNavigation);
    }
}

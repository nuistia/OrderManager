using Hometask.OrderManager.Data.EF.Models;

namespace Hometask.OrderManager.Core.Services.Orders.EF;

public interface IOrderService
{
    void AddOrder(Order order);
    void DeleteOrder(Order order);
    void UpdateOrder(Order order);
    List<Order> GetOrders();
    Order GetOrderById(Guid id);
    List<Order> GetLastYearOrders();
}

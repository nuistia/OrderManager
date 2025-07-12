using Hometask.OrderManager.Core.Services.Orders.EF;
using Hometask.OrderManager.Data.ADO.Repositories;
using Hometask.OrderManager.Data.Models;

namespace Hometask.OrderManager.Core.Services.Orders.ADO;

public class OrderService : IOrderService
{
    private readonly OrderRepository _repository;

    public OrderService(OrderRepository repository)
    {
        _repository = repository;
    }

    public void AddOrder(Order order)
    {
        _repository.AddOrder(order);
    }

    public void DeleteOrder(Order order)
    {
        _repository.DeleteOrder(order);
    }

    public List<Order> GetLastYearOrders()
    {
        return _repository.GetOrdersLastYear();
    }

    public Order GetOrderById(Guid id)
    {
        return _repository.GetOrderById(id)
            ?? throw new KeyNotFoundException($"Order with id {id} not found.");
    }

    public List<Order> GetOrders()
    {
        return _repository.GetOrders();
    }

    public void UpdateOrder(Order order)
    {
        _repository.UpdateOrder(order);
    }
}

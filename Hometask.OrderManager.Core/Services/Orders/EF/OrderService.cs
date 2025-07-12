using Hometask.OrderManager.Data.Models;
using Hometask.OrderManager.Data.EF.Repositories;
using Hometask.OrderManager.Data.EF.Repositories.Specifications.Orders;

namespace Hometask.OrderManager.Core.Services.Orders.EF;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _repository;

    public OrderService(IRepository<Order> repository)
    {
        _repository = repository ?? throw new ArgumentException();
    }

    public void AddOrder(Order order)
    {
        _repository.Add(order);
    }

    public void DeleteOrder(Order order)
    {
        _repository.Delete(order);
    }

    public List<Order> GetLastYearOrders()
    {
        return _repository.GetBySpec(new OrderLastYearSpecification());
    }

    public Order GetOrderById(Guid id)
    {
        return _repository.GetById(id);
    }

    public List<Order> GetOrders()
    {
        return _repository.GetAll().ToList();
    }

    public void UpdateOrder(Order order)
    {
        _repository.Update(order);
    }
}
